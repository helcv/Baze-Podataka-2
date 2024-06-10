using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Entities
{
    public class Bank
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SwiftCode { get; set; }

        public ICollection<Branch> Branches { get; set; }
    }
}
