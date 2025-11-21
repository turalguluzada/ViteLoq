using Restaurant.Domain.Card.Entities;

namespace Restaurant.Domain.Card.Interfaces;

public interface IBankCardRepository
{
    public void CreateBankCard(BankCard bankCard);
    public BankCard? GetBankCard(string bankCardId);
    public void UpdateBankCard(BankCard bankCard);
    public void DeleteBankCard(string bankCardId);
    public BankCard? GetBankCardByNumber(string bankCardNumber);
    public void ChangeBalance(BankCard bankCard, int balance);
    public void RefreshBankCardDb();
}