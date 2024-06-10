using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

        public long CardNumber { get; set; }
        public Card Card { get; set; }
    }
}
