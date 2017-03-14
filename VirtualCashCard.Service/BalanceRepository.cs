namespace VirtualCashCard.Service
{
    /// <summary>
    /// <see cref="IBalanceRepository"/> 
    /// </summary>
    internal class BalanceRepository : IBalanceRepository
    {
        private int _balance;
        
        /// <summary>
        /// <see cref="IBalanceRepository.GetBalance"/> 
        /// </summary>
        public int GetBalance()
        {
            return _balance;
        }

        /// <summary>
        /// <see cref="IBalanceRepository.InitBalance(int)"/> 
        /// </summary>
        public void InitBalance(int balance)
        {
            _balance = balance;
        }

        /// <summary>
        /// <see cref="IBalanceRepository.UpdateBalance(int)"/> 
        /// </summary>
        public void UpdateBalance(int newBalance)
        {
            _balance = newBalance;
        }
    }
}
