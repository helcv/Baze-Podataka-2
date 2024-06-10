using BankingSystem.Common;
using BankingSystem.Entities;

namespace BankingSystem.DTOs
{
    public class CardDto
    {
        public long CardNumber { get; set; }
        public double Balance { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CVV { get; set; }
        public CardType Type { get; set; }
    }
}
