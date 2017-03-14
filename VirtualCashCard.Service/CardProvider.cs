using System.Configuration;
using VirtualCashCard.Service.Configuration;

namespace VirtualCashCard.Service
{
    /// <summary>
    /// <see cref="ICardProvider"/> 
    /// </summary>
    public class CardProvider : ICardProvider
    {
        private ICard _card = null;
        
        /// <summary>
        /// <see cref="ICardProvider.GetCard"/> 
        /// </summary>
        public ICard GetCard()
        {
            if (_card != null) return _card;

            var cardConfig = ConfigurationManager.GetSection("cardConfiguration") as CardConfig;
            var balanceRepository = new BalanceRepository();

            return new Card(cardConfig, balanceRepository);
        }
    }
}
