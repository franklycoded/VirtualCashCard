using System;
using VirtualCashCard.Service;

namespace VirtualCashCard.Application
{
    // This code is only for real-life testing the Cash Card implementation, hence it's not unit-tested
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initialising cash card...");

            var cardProvider = new CardProvider();
            var card = cardProvider.GetCard();

            Console.WriteLine("Cash card initialised!");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose from the following options:\n1. Top up card\n2. Withdraw money\n3. Exit");

                var result = Console.ReadKey(true);

                if(result.KeyChar == '1')
                {
                    TopUp(card);
                }
                else if(result.KeyChar == '2')
                {
                    Withdraw(card);
                }
                else if(result.KeyChar == '3')
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        static void TopUp(ICard card)
        {
            Console.WriteLine("How much would you like to top up?");

            var result = Console.ReadLine();

            int topupAmount;

            if(Int32.TryParse(result, out topupAmount) == false)
            {
                Console.WriteLine("Invalid topup amount!");
                return;
            }

            try
            {
                var topupResult = card.TopUp(topupAmount);
                Console.WriteLine("Top up successful, the new balance is: " + topupResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while topping up: " + ex.Message);
                return;
            }
        }

        static void Withdraw(ICard card)
        {
            Console.WriteLine("What is your PIN?");

            var pinText = Console.ReadLine();

            int pin;

            if (Int32.TryParse(pinText, out pin) == false)
            {
                Console.WriteLine("Invalid pin number!");
                return;
            }

            Console.WriteLine("How much would you like to withdraw?");

            var amount = Console.ReadLine();

            int withdrawAmount;

            if (Int32.TryParse(amount, out withdrawAmount) == false)
            {
                Console.WriteLine("Invalid withdraw amount!");
                return;
            }

            try
            {
                int newBalance;

                var withdrawResult = card.WithdrawMoney(pin, withdrawAmount, out newBalance);

                if(withdrawResult.Item1 == false)
                {
                    Console.WriteLine("Error while withdrawing money: " + withdrawResult.Item2);
                    return;
                }

                Console.WriteLine("Successful withdrawal, new balance: " + newBalance);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while withdrawing money: " + ex.Message);
            }
        }
    }
}
