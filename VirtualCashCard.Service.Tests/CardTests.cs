using Moq;
using NUnit.Framework;
using System;
using VirtualCashCard.Service.Configuration;

namespace VirtualCashCard.Service.Tests
{
    [TestFixture]
    public class CardTests
    {
        private Mock<ICardConfig> _mockCardConfig;
        private Mock<IBalanceRepository> _mockBalanceRepository;

        [SetUp]
        public void Init()
        {
            _mockCardConfig = new Mock<ICardConfig>();
            _mockBalanceRepository = new Mock<IBalanceRepository>();
        }

        [Test]
        public void Test_Card_NoCardConfigProvided_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var card = new Card(null, _mockBalanceRepository.Object);
            });
        }

        [Test]
        public void Test_Card_NoBalanceRepositoryProvided_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var card = new Card(_mockCardConfig.Object, null);
            });
        }

        [Test]
        public void Test_Card_Init_BalanceInitialised()
        {
            _mockCardConfig.SetupGet(m => m.InitialAmount).Returns(100);

            var card = new Card(_mockCardConfig.Object, _mockBalanceRepository.Object);

            _mockBalanceRepository.Verify(m => m.InitBalance(100), Times.Once);
        }

        [Test]
        public void Test_Card_TopUp_Success_NewBalanceUpdatedAndReturned()
        {
            _mockBalanceRepository.Setup(m => m.GetBalance()).Returns(100);

            var card = new Card(_mockCardConfig.Object, _mockBalanceRepository.Object);

            var result = card.TopUp(20);

            Assert.AreEqual(120, result);
            _mockBalanceRepository.Verify(m => m.UpdateBalance(120), Times.Once);
        }

        [Test]
        public void Test_Card_TopUp_NegativeAmount_ArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var card = new Card(_mockCardConfig.Object, _mockBalanceRepository.Object);

                card.TopUp(-15);
            });

            _mockBalanceRepository.Verify(m => m.UpdateBalance(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void Test_Card_WithdrawMoney_NegativeAmount_ArgumentOutOfRangeException()
        {
            _mockBalanceRepository.Setup(m => m.GetBalance()).Returns(100);
            _mockCardConfig.SetupGet(m => m.Pin).Returns(4444);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var card = new Card(_mockCardConfig.Object, _mockBalanceRepository.Object);

                int newBalance;

                card.WithdrawMoney(4444, -22, out newBalance);
            });
        }

        [Test]
        public void Test_Card_WithdrawMoney_WrongPin_ReturnFalse()
        {
            _mockBalanceRepository.Setup(m => m.GetBalance()).Returns(100);
            _mockCardConfig.SetupGet(m => m.Pin).Returns(4444);

            var card = new Card(_mockCardConfig.Object, _mockBalanceRepository.Object);

            int newBalance;

            var result = card.WithdrawMoney(2222, 20, out newBalance);

            Assert.IsFalse(result.Item1);
            Assert.AreEqual("The PIN code is invalid!", result.Item2);
            Assert.AreEqual(100, newBalance);
            _mockBalanceRepository.Verify(m => m.UpdateBalance(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void Test_Card_WithdrawMoney_NotEnoughMoney_ReturnFalse()
        {
            _mockBalanceRepository.Setup(m => m.GetBalance()).Returns(100);
            _mockCardConfig.SetupGet(m => m.Pin).Returns(2222);

            var card = new Card(_mockCardConfig.Object, _mockBalanceRepository.Object);

            int newBalance;

            var result = card.WithdrawMoney(2222, 1500, out newBalance);

            Assert.IsFalse(result.Item1);
            Assert.AreEqual("There's not enough money on the account for this transaction!", result.Item2);
            Assert.AreEqual(100, newBalance);
            _mockBalanceRepository.Verify(m => m.UpdateBalance(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void Test_Card_WithdrawMoney_Success_ReturnTrue()
        {
            _mockBalanceRepository.Setup(m => m.GetBalance()).Returns(100);
            _mockCardConfig.SetupGet(m => m.Pin).Returns(2222);

            var card = new Card(_mockCardConfig.Object, _mockBalanceRepository.Object);

            int newBalance;

            var result = card.WithdrawMoney(2222, 20, out newBalance);

            Assert.IsTrue(result.Item1);
            Assert.AreEqual(80, newBalance);
            _mockBalanceRepository.Verify(m => m.UpdateBalance(80), Times.Once);
        }
    }
}