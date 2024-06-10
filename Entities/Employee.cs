using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
