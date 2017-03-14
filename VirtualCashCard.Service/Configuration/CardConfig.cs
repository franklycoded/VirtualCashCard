using System;
using System.Configuration;

namespace VirtualCashCard.Service.Configuration
{
    /// <summary>
    /// <see cref="ICardConfig"/> 
    /// </summary>
    internal class CardConfig : ConfigurationSection, ICardConfig
    {
        /// <summary>
        /// <see cref="ICardConfig.InitialAmount"/> 
        /// </summary>
        [ConfigurationProperty("initialAmount", IsRequired = true)]
        public int InitialAmount
        {
            get
            {
                return Convert.ToInt32(this["initialAmount"]);
            }

            set
            {
                this["initialAmount"] = value;
            }
        }

        /// <summary>
        /// <see cref="ICardConfig.Pin"/> 
        /// </summary>
        [ConfigurationProperty("pin", IsRequired = true)]
        public int Pin
        {
            get
            {
                return Convert.ToInt32(this["pin"]);
            }

            set
            {
                this["pin"] = value;
            }
        }
    }
}
