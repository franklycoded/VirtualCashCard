using System;
using VirtualCashCard.Service.Configuration;

namespace VirtualCashCard.Service
{
    /// <summary>
    /// <see cref="ICard"/> 
    /// </summary>
    internal class Card : ICard
    {
        private object _cardLock = new object();

        protected readonly ICardConfig _cardConfig;
        protected readonly IBalanceRepository _balanceRepository;

        public Card(ICardConfig cardConfig, IBalanceRepository balanceRepository)
        {
            if (cardConfig == null) throw new ArgumentNullException(nameof(cardConfig));
            if (balanceRepository == null) throw new ArgumentNullException(nameof(balanceRepository));

            _cardConfig = cardConfig;
            _balanceRepository = balanceRepository;

            _balanceRepository.InitBalance(_cardConfig.InitialAmount);
        }
        
        /// <summary>
        /// <see cref="ICard.TopUp(int)"/> 
        /// </summary>
        public int TopUp(int amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "The topup amount has to be a positive value!");

            lock (_cardLock)
            {
                var actualBalance = _balanceRepository.GetBalance();

                actualBalance += amount;

                _balanceRepository.UpdateBalance(actualBalance);

                return actualBalance;
            }
        }

        /// <summary>
        /// <see cref="ICard.WithdrawMoney(int, int, out int)"/>
        /// </summary>
        public Tuple<bool, string> WithdrawMoney(int pin, int amount, out int newBalance)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "The withdraw amount has to be a positive value!");

            lock (_cardLock)
            {
                var actualBalance = _balanceRepository.GetBalance();

                if (pin != _cardConfig.Pin)
                {
                    newBalance = actualBalance;
                    return new Tuple<bool, string>(false, "The PIN code is invalid!");
                }

                if (actualBalance < amount)
                {
                    newBalance = actualBalance;
                    return new Tuple<bool, string>(false, "There's not enough money on the account for this transaction!");
                }

                actualBalance -= amount;

                _balanceRepository.UpdateBalance(actualBalance);
                newBalance = actualBalance;
                return new Tuple<bool, string>(true, null);
            }
        }
    }
}
