using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BankingSystem.Common;

namespace BankingSystem.Entities
{
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CardNumber { get; set; }
        public double Balance { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CVV { get; set; }
        public CardType Type { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
