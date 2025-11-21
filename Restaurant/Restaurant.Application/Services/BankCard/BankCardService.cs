using Restaurant.Application.Interfaces.BankCard;
using Restaurant.Domain.Card.Interfaces;
using Restaurant.Domain.User.Entities;
using Restaurant.Domain.User.Interfaces;
using Restaurant.Shared.Extensions;

namespace Restaurant.Application.Services.BankCard;

public class BankCardService : IBankCardService
{
    private readonly IBankCardRepository _bankCardRepository;

    public BankCardService(IBankCardRepository bankCardRepository)
    {
        _bankCardRepository = bankCardRepository;
    }

    public void CreateBankCard(AppUser appUser)
    {
        Domain.Card.Entities.BankCard bankCard = new Domain.Card.Entities.BankCard();
        bankCard.Id = Guid.NewGuid();
        tryCardNumber:
        bankCard.CardNumber = Helpers.GiveConditionalNumber("bank card number", 16, "0000 - 0000 - 0000 - 0000");
        if (UnicBankCardNumber(bankCard.CardNumber))
        {
            Console.WriteLine("Bank Card Number Already Exists.Please try again");
            goto tryCardNumber;
        }

        bankCard.ExpirationDate = Helpers.GiveDate("card expiration date");
        bankCard.Cvv = Helpers.GiveConditionalNumber("card cvv", 3, "000");
        bankCard.Pin = Convert.ToInt32(Helpers.GiveConditionalNumber("card pin", 4, "0000"));
        bankCard.Balance = 0;
        bankCard.UserId = appUser.Id;

        _bankCardRepository.CreateBankCard(bankCard);
    }

    public Domain.Card.Entities.BankCard? GetBankCard(string cardNumber)
    {
        return _bankCardRepository.GetBankCard(cardNumber);
    }

    public void UpdateBankCard(Domain.Card.Entities.BankCard bankCard)
    {
        _bankCardRepository.UpdateBankCard(bankCard);
    }

    public void DeleteBankCard(string cardNumber)
    {
        _bankCardRepository.DeleteBankCard(cardNumber);
    }

    public bool IncreaseBalance(string cardNumber, int amount)
    {
        Console.Clear();
        var bankCard = _bankCardRepository.GetBankCard(cardNumber);
        if (bankCard == null)
        {
            Console.WriteLine("Bank Card Not Found");
            return false;
        }
        int cardPin = Helpers.GiveNumber("card pin");
        if (amount < 1)
        {
            Console.WriteLine("Amount is less than 1");
            return false;
        }
        if (bankCard.Pin != cardPin)
        {
            Console.WriteLine("Card Pin Incorrect");
            return false;
        }
        if (bankCard.Balance + amount >= 100000)
        {
            Console.WriteLine("Your balance max limit is 100000.");
            return false;
        }

        bankCard.Balance += amount;
        _bankCardRepository.RefreshBankCardDb();
        return true;
    }

    public bool DecreaseBalance(string cardNumber, int amount)
    {
        var bankCard = _bankCardRepository.GetBankCard(cardNumber);
        if (bankCard == null)
        {
            Console.WriteLine("Bank Card Not Found");
            return false;
        }

        if (amount < 1)
        {
            Console.WriteLine("Amount is less than 1");
            return false;
        }

        if (bankCard.Balance - amount <= 0)
        {
            Console.WriteLine("Your balance min limit is 0.");
            return false;
        }

        bankCard.Balance -= amount;
        return true;
    }

    public bool UnicBankCardNumber(string bankCardNumber)
    {
        Domain.Card.Entities.BankCard bankCard = _bankCardRepository.GetBankCardByNumber(bankCardNumber);
        if (bankCard == null)
            return false;
        return true;
    }

    public bool TransferMoney(string sendingCardNumber, string receiverCardNumber, int amount)
    {
        var senderCard = _bankCardRepository.GetBankCardByNumber(sendingCardNumber);
        var receiverCard = _bankCardRepository.GetBankCardByNumber(receiverCardNumber);

        if (senderCard == null)
        {
            Console.WriteLine("Sender Card Not Found");
            return false;
        }

        if (receiverCard == null)
        {
            Console.WriteLine("Receiver Card Not Found");
            return false;
        }

        if (DecreaseBalance(sendingCardNumber, amount))
        {
            if (!IncreaseBalance(receiverCardNumber, amount))
            {
                IncreaseBalance(sendingCardNumber, amount);
                Console.Clear();
                Console.WriteLine("Receiver balance max limit is 100000.");
                return false;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Your money successfully transferred !");
                return true;
            }
        }
        return false;   
    }

}