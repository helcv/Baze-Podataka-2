using AutoMapper;
using BankingSystem.DTOs;
using BankingSystem.Entities;
using BankingSystem.Extensions;
using BankingSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, 
            ITokenService tokenService, 
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _contextAccessor = httpContextAccessor;
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            var userQuery = _unitOfWork.Users.AsQueryable();
            var user = await userQuery.SingleOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null) return Unauthorized("Invalid email!");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (user.HashedPassword[i] != computedHash[i]) return Unauthorized("Invalid password!");
            }

            return Ok(new TokenDto { Token = _tokenService.CreateToken(user) });
        }

        [Authorize]
        [HttpPost("accounts/checkings")]
        public async Task<IActionResult> CreateCheckingsAccount()
        {
            var currUser = _contextAccessor.HttpContext.User;
            var currId = currUser.GetUserId();

            var user = await _unitOfWork.Users.GetByIdAsync(currId);
            if (user == null) return NotFound();

            var account = new CheckingsAccount
            {
                UserId = currId,
                Balance = 0,
                CreatedDate = DateTime.Now,
                OverdraftLimit = 0
            };

            await _unitOfWork.Accounts.AddAsync(account);
            await _unitOfWork.SaveAsync();

            return Ok("Account successfully created");
        }

        [Authorize]
        [HttpPost("accounts/savings")]
        public async Task<IActionResult> CreateSavingsAccount()
        {
            var currUser = _contextAccessor.HttpContext.User;
            var currId = currUser.GetUserId();

            var user = await _unitOfWork.Users.GetByIdAsync(currId);
            if (user == null) return NotFound();

            var account = new SavingsAccount
            {
                UserId = currId,
                Balance = 0,
                CreatedDate = DateTime.Now,
                InterestRate = 0
            };

            await _unitOfWork.Accounts.AddAsync(account);
            await _unitOfWork.SaveAsync();

            return Ok("Account successfully created");
        }

        [Authorize]
        [HttpGet("accounts/savings")]
        public async Task<IActionResult> GetAllSavingsAccounts()
        {
            var currUser = _contextAccessor.HttpContext.User;
            var currId = currUser.GetUserId();

            var user = await _unitOfWork.Users.GetByIdAsync(currId);
            if (user == null) return NotFound();

            var accQuery = _unitOfWork.Accounts.AsQueryable();
            var savingsAccs = await accQuery
                .OfType<SavingsAccount>()
                .ToListAsync();

            return Ok(_mapper.Map<List<SavingsDto>>(savingsAccs));
        }

        [Authorize]
        [HttpGet("accounts/checkings")]
        public async Task<IActionResult> GetAllCheckingsAccounts()
        {
            var currUser = _contextAccessor.HttpContext.User;
            var currId = currUser.GetUserId();

            var user = await _unitOfWork.Users.GetByIdAsync(currId);
            if (user == null) return NotFound();

            var accQuery = _unitOfWork.Accounts.AsQueryable();
            var checkingsAccs = await accQuery
                .OfType<CheckingsAccount>()
                .ToListAsync();

            return Ok(_mapper.Map<List<CheckingsDto>>(checkingsAccs));
        }

        [Authorize]
        [HttpPost("loan")]
        public async Task<IActionResult> CreateLoan(CreateLoanDto createLoanDto)
        {
            var currUser = _contextAccessor.HttpContext.User;
            var currId = currUser.GetUserId();

            var user = await _unitOfWork.Users.GetByIdAsync(currId);
            if (user == null) return NotFound();

            if (createLoanDto.Amount > 5000)
                return BadRequest("Not allowed to loan that much money.");

            var userQuery = _unitOfWork.Users.AsQueryable();
            var userWithEmployee = await userQuery.Include(u => u.Employee).SingleOrDefaultAsync(u => u.Id == currId);

            var branchId = userWithEmployee.Employee.BranchId;

            var loan = new Loan
            {
                Amount = createLoanDto.Amount,
                RepaymentDate = DateTime.Now.AddYears(3),
                UserId = currId,
                BranchId = branchId
            };

            await _unitOfWork.Loans.AddAsync(loan);
            await _unitOfWork.SaveAsync();

            return Ok("Loan successfully created");
        }

        [Authorize]
        [HttpGet("loans")]
        public async Task<IActionResult> GetAllLoans([FromQuery] string status = null)
        {
            var currUser = _contextAccessor.HttpContext.User;
            var currId = currUser.GetUserId();

            var user = await _unitOfWork.Users.GetByIdAsync(currId);
            if (user == null) return NotFound();

            var loanQuery = _unitOfWork.Loans.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                switch (status.ToLower())
                {
                    case "paid":
                        loanQuery = loanQuery.Where(l => l.Amount == 0);
                        break;
                    case "unpaid":
                        loanQuery = loanQuery.Where(l => l.Amount != 0);
                        break;
                    default:
                        return BadRequest("Invalid status value. Use 'paid' or 'unpaid'.");
                }
            }

            var loans = await loanQuery.Include(l => l.Payments).Where(l => l.UserId == currId).ToListAsync();

            return Ok(_mapper.Map<List<LoanDto>>(loans));
        }

        [Authorize]
        [HttpGet("{loanId}/loan/payments")]
        public async Task<IActionResult> GetAllPaymentsForLoan(int loanId, [FromQuery] string sort = "asc")
        {
            var currUser = _contextAccessor.HttpContext.User;
            var currId = currUser.GetUserId();

            var user = await _unitOfWork.Users.GetByIdAsync(currId);
            if (user == null) return NotFound();

            var paymentQuery = _unitOfWork.Payments.AsQueryable();

            if (sort.ToLower() == "desc")
            {
                paymentQuery = paymentQuery.Where(p => p.LoanId == loanId && p.UserId == currId).OrderByDescending(p => p.ExecutionDate);
            }
            else
            {
                paymentQuery = paymentQuery.Where(p => p.LoanId == loanId && p.UserId == currId).OrderBy(p => p.ExecutionDate);
            }

            var payments = await paymentQuery.ToListAsync();

            return Ok(_mapper.Map<List<PaymentDto>>(payments));
        }

        [Authorize]
        [HttpPost("{loanId}/payment")]
        public async Task<IActionResult> PayOffLoan(int loanId, CreatePaymentDto createPaymentDto)
        {
            var currUser = _contextAccessor.HttpContext.User;
            var currId = currUser.GetUserId();

            var user = await _unitOfWork.Users.GetByIdAsync(currId);
            if (user == null) return NotFound();

            var loan = await _unitOfWork.Loans.GetByIdAsync(loanId);
            if ((loan.Amount - createPaymentDto.Amount) < 0)
            {
                return BadRequest("You can not overpay a loan.");
            }

            if (loan.UserId != currId)
            {
                return BadRequest("You can not pay somebody else's loan.");
            }

            var payment = new Payment
            {
                ExecutionDate = DateTime.Now,
                Amount = createPaymentDto.Amount,
                Loan = loan,
                LoanId = loan.Id,
                User = user,
                UserId = currId,
            };

            loan.Amount -= createPaymentDto.Amount;

            _unitOfWork.Loans.Update(loan);
            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.SaveAsync();

            loan.Payments.Add(payment);
            await _unitOfWork.SaveAsync();

            return Ok("Payment successfully applied.");
        }
    }
}
