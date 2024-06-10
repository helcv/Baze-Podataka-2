using AutoMapper;
using BankingSystem.DTOs;
using BankingSystem.Entities;

namespace BankingSystem.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Loan, LoanDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Card, CardDto>().ReverseMap();
            CreateMap<SavingsAccount, SavingsDto>().ReverseMap();
            CreateMap<CheckingsAccount, CheckingsDto>().ReverseMap();
        }
    }
}
