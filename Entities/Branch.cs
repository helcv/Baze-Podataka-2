using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Entities
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public int BankId { get; set; }
        public Bank Bank { get; set; }

        public ICollection<Loan> Loans { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
