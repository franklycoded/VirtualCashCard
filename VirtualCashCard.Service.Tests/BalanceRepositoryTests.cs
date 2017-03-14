using NUnit.Framework;

namespace VirtualCashCard.Service.Tests
{
    /// <summary>
    /// Tests to verify the operational correctness of the demo repository
    /// </summary>
    [TestFixture]
    public class BalanceRepositoryTests
    {
        [Test]
        public void Test_BalanceRepository_InitBalanceStored()
        {
            var repo = new BalanceRepository();

            Assert.AreEqual(0, repo.GetBalance());

            repo.InitBalance(100);

            Assert.AreEqual(100, repo.GetBalance());
        }

        [Test]
        public void Test_BalanceRepository_UpdateBalanceStored()
        {
            var repo = new BalanceRepository();

            repo.InitBalance(100);

            Assert.AreEqual(100, repo.GetBalance());

            repo.UpdateBalance(200);

            Assert.AreEqual(200, repo.GetBalance());
        }
    }
}
