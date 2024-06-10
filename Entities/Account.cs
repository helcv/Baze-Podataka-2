using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Entities
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public double Balance { get; set; }
        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
