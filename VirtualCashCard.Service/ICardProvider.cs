namespace VirtualCashCard.Service
{
    /// <summary>
    /// Provider class to retrieve the Cash Card interface
    /// For demo purposes: instead of hosting a WebAPI service, we return the interface of the Card class
    /// </summary>
    public interface ICardProvider
    {
        /// <summary>
        /// Gets the Card
        /// </summary>
        /// <returns>The Card wrapped in it's interface</returns>
        ICard GetCard();
    }
}
