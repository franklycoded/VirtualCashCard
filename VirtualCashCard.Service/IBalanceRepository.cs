namespace VirtualCashCard.Service
{
    /// <summary>
    /// Class to persist the cash balance - in memory only for demo purposes
    /// </summary>
    public interface IBalanceRepository
    {
        /// <summary>
        /// Returns the actual balance
        /// </summary>
        /// <returns>The actual balance</returns>
        int GetBalance();
        
        /// <summary>
        /// Updates the cash balance
        /// </summary>
        /// <param name="newBalance">The new cash balance</param>
        void UpdateBalance(int newBalance);

        /// <summary>
        /// Initialises the cash balance
        /// </summary>
        /// <param name="balance">The new cash balance</param>
        void InitBalance(int balance);
    }
}
