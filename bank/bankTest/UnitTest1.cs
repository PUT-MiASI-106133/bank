using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bank;

namespace bankTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void NewBankMakeTest()
        {
            //Arrange
            CKIR KIR = new CKIR();
            CBank bank = new CBank(KIR);

            //Assert
            Assert.IsNotNull(bank);
        }

        [TestMethod]
        public void newCustomerTest()
        {
            //Arrange
            CKIR KIR = new CKIR();
            CBank bank = new CBank(KIR);
            bank.AddCustomer("Ivan", "Pavlov", 1);

            //Assert
            Assert.IsNotNull(bank.GetCustomerList());
            Assert.AreEqual(bank.GetCustomer(1).GetName(), "Ivan");
            Assert.AreEqual(bank.GetCustomer(1).GetSurname(), "Pavlov");
            Assert.AreEqual(bank.GetCustomer(1).GetCustomerID(), 1);
        }

        [TestMethod]
        public void newAccountTest()
        {
            //Arrange
            CKIR KIR = new CKIR();
            CBank bank = new CBank(KIR);
            bank.AddCustomer("Ivan", "Pavlov", 1);
            bank.StoreAccount(1, 1);

            //Assert
            Assert.IsNotNull(bank.GetAccount(1));
            Assert.AreEqual(bank.GetAccount(1).GetAccID(), 1);
            Assert.AreEqual(bank.GetAccount(1).GetSaldo(), 0m);
        }

        [TestMethod]
        public void PayInTest()
        {
            //Arrange
            CKIR KIR = new CKIR();
            CBank bank = new CBank(KIR);
            bank.AddCustomer("Ivan", "Pavlov", 1);
            bank.StoreAccount(1, 1);
            bank.PayIn(bank.GetAccount(1), 500m);

            //Assert
            Assert.AreEqual(bank.GetAccount(1).GetSaldo(), 500m);
        }

        [TestMethod]
        public void WithDrawGoodTest()
        {
            //Arrange
            CKIR KIR = new CKIR();
            CBank bank = new CBank(KIR);
            bank.AddCustomer("Ivan", "Pavlov", 1);
            bank.StoreAccount(1, 1);
            bank.PayIn(bank.GetAccount(1), 500m);
            bank.WithDraw(bank, bank.GetAccount(1), 250m);

            //Assert
            Assert.AreEqual(bank.GetAccount(1).GetSaldo(), 250m);
        }

        [TestMethod]
        public void WithDrawDebitTest()
        {
            //Arrange
            CKIR KIR = new CKIR();
            CBank bank = new CBank(KIR);
            bank.AddCustomer("Ivan", "Pavlov", 1);
            bank.StoreAccount(1, 1);
            bank.PayIn(bank.GetAccount(1), 500m);
            IAccount decBank = new DecoratorDebit(bank);
            decBank.WithDraw(bank, bank.GetAccount(1), 600m);

            //Assert
            Assert.AreEqual(bank.GetAccount(1).GetSaldo(), -100m);
        }

        [TestMethod]
        public void InnerTransferTest()
        {
            //Arrange
            CKIR KIR = new CKIR();
            CBank bank = new CBank(KIR);
            bank.AddCustomer("Ivan", "Pavlov", 1);
            bank.StoreAccount(1, 1);
            bank.AddCustomer("Rafal", "Swierczewsky", 2);
            bank.StoreAccount(2, 2);
            bank.PayIn(bank.GetAccount(1), 600m);
            bank.Transfer(bank.GetAccount(1), bank.GetAccount(2), 500m);

            //Assert
            Assert.AreEqual(bank.GetAccount(2).GetSaldo(), 500m);
            Assert.IsNotNull(bank.GetAccount(1).GetHistory());
            Assert.AreEqual(bank.GetAccount(1).GetSaldo(), 100m);
        }

        [TestMethod]
        public void KIRAddBankTest()
        {
            //Arrange
            CKIR KIR = new CKIR();
            CBank bank1 = new CBank(KIR);
            CBank bank2 = new CBank(KIR);
            KIR.AddBank(bank1);
            KIR.AddBank(bank2);

            //Assert
            Assert.IsTrue(KIR.IsBankExist(bank1));
            Assert.IsTrue(KIR.IsBankExist(bank2));
            
        }


        [TestMethod]
        public void OuterTransferTest()
        {
            //Arrange
            CKIR KIR = new CKIR();
            CBank bank1 = new CBank(KIR);
            CBank bank2 = new CBank(KIR);
            KIR.AddBank(bank1);
            KIR.AddBank(bank2);
            bank1.AddCustomer("Ivan", "Pavlov", 1);
            bank1.StoreAccount(1, 1);
            bank2.AddCustomer("Rafal", "Swierczewsky", 2);
            bank2.StoreAccount(2, 2);
            bank1.PayIn(bank1.GetAccount(1), 600m);

            bank1.Transfer(bank1.GetAccount(1), bank2.GetAccount(2), 500m);
            bank1.Send();
            KIR.Send();

            //Assert
            Assert.AreEqual(bank2.GetAccount(2).GetSaldo(), 500m);
            Assert.AreEqual(bank1.GetAccount(1).GetSaldo(), 100m);
        }

        [TestMethod]
        public void LowInterestTest()
        {
            CKIR KIR = new CKIR();
            CBank bank = new CBank(KIR);
            bank.AddCustomer("Ivan", "Pavlov", 1);
            bank.StoreAccount(1, 1);
            bank.PayIn(bank.GetAccount(1), 500m);

            //Assert
            Assert.AreEqual(bank.GetAccount(1).GetSaldo(), 500m);

            bank.GetAccount(1).Request();

            Assert.AreEqual(bank.GetAccount(1).GetSaldo(), 550m);
        }

        [TestMethod]
        public void AverageInterestTest()
        {
            CKIR KIR = new CKIR();
            CBank bank = new CBank(KIR);
            bank.AddCustomer("Ivan", "Pavlov", 1);
            bank.StoreAccount(1, 1);
            bank.PayIn(bank.GetAccount(1), 2000m);

            //Assert
            Assert.AreEqual(bank.GetAccount(1).GetSaldo(), 2000m);

            bank.GetAccount(1).Request();

            Assert.AreEqual(bank.GetAccount(1).GetSaldo(), 2400m);
        }

        [TestMethod]
        public void HighInterestTest()
        {
            CKIR KIR = new CKIR();
            CBank bank = new CBank(KIR);
            bank.AddCustomer("Ivan", "Pavlov", 1);
            bank.StoreAccount(1, 1);
            bank.PayIn(bank.GetAccount(1), 20000m);

            //Assert
            Assert.AreEqual(bank.GetAccount(1).GetSaldo(), 20000m);

            bank.GetAccount(1).Request();

            Assert.AreEqual(bank.GetAccount(1).GetSaldo(), 26000m);
        }
        
    }
}
