using System;
namespace Restaurant.Domain.Card.Entities
{
    public class BankCard
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Cvv { get; set; }
        public int Pin { get; set; }
        public int Balance { get; set; }
        public Guid UserId { get; set; }
    }
}

