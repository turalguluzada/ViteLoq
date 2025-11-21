using Restaurant.Domain.User.Entities;

namespace Restaurant.Application.Interfaces.BankCard;

public interface IBankCardService
{
    public void CreateBankCard(AppUser appUser);
    public Domain.Card.Entities.BankCard? GetBankCard(string cardNumber);
    public void UpdateBankCard(Domain.Card.Entities.BankCard bankCard);
    public void DeleteBankCard(string cardNumber);
    public bool IncreaseBalance(string cardNumber, int amount);
    public bool DecreaseBalance(string cardNumber, int amount);
    public bool TransferMoney(string sendingCardNumber, string receiverCardNumber, int amount);
}