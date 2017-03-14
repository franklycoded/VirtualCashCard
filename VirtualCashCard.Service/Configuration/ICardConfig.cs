namespace VirtualCashCard.Service.Configuration
{
    /// <summary>
    /// Class to hold the initial configuration data for the cash card
    /// </summary>
    public interface ICardConfig
    {
        /// <summary>
        /// The PIN code of the card
        /// </summary>
        int Pin { get; set; }

        /// <summary>
        /// The initial amount of money on the account
        /// </summary>
        int InitialAmount { get; set; }
    }
}
