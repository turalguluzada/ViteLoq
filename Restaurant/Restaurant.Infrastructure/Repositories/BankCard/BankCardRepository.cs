using Restaurant.Domain.Card.Interfaces;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.BankCard;

public class BankCardRepository : IBankCardRepository
{
    public List<Domain.Card.Entities.BankCard> BankCards = Context.AppDbContext.BankCards;
    
    public BankCardRepository()
    {
        if (Context.AppDbContext.BankCards.Count < 1)
        {
            // Context.AppDbContext.RefreshBankCardList();
            BankCards = Context.AppDbContext.BankCards;
        }
    }
    
    public void CreateBankCard(Domain.Card.Entities.BankCard bankCard)
    {
        // BankCards.Add(bankCard);
        BankCards.Add(bankCard);
        // Context.AppDbContext.BankCards.Add(bankCard);
        Context.AppDbContext.RefreshBankCardDb();
    }

    public Domain.Card.Entities.BankCard? GetBankCard(string cardNumber)
    {
        for (int i = 0; i < BankCards.Count; i++)
        {
            if (BankCards[i].CardNumber == cardNumber)
            {
                return BankCards[i];
            }
        }
        return null;
    }

    public void UpdateBankCard(Domain.Card.Entities.BankCard bankCard)
    {
        for (int i = 0; i < BankCards.Count; i++)
        {
            if (BankCards[i].Id == bankCard.Id)
            {
                BankCards[i] = bankCard;
            }
        }
    }

    public void DeleteBankCard(string cardNumber)
    {
        for (int i = 0; i < BankCards.Count; i++)
        {
            if (BankCards[i].CardNumber == cardNumber)
            {
                BankCards.Remove(BankCards[i]);
            }
        }
    }

    public Domain.Card.Entities.BankCard GetBankCardByNumber(string cardNumber)
    {
        for (int i = 0; i < BankCards.Count; i++)
        {
            if (BankCards[i].CardNumber == cardNumber)
            {
                return BankCards[i];
            }
        }
        return null;
    }

    public void ChangeBalance(Domain.Card.Entities.BankCard bankCard, int balance)
    {
        bankCard.Balance += balance;
    }

    public void RefreshBankCardDb()
    {
        Context.AppDbContext.RefreshBankCardDb();
    }
}