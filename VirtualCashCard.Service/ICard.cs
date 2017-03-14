using System;

namespace VirtualCashCard.Service
{
    /// <summary>
    /// The class representing the Virtual Cash Card
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// Withdraws money from the cash card
        /// </summary>
        /// <param name="pin">The PIN code provided</param>
        /// <param name="amount">The amount to withdraw</param>
        /// <param name="newBalance">The new cash balance</param>
        /// <returns>True if the operation was successful, false and an error message otherwise</returns>
        Tuple<bool, string> WithdrawMoney(int pin, int amount, out int newBalance);

        /// <summary>
        /// Tops up the cash card with an arbitary amount
        /// </summary>
        /// <param name="amount">The amount to top up with</param>
        /// <returns>The new balance</returns>
        int TopUp(int amount);
    }
}
