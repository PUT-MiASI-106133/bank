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
         //   Assert.IsTrue(bank.GetCustomer(1).GetAccounts().Contains(1));
         //   Assert.IsFalse(bank.GetCustomer(1).GetAccountsID().Contains(2));
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
            bank.WithDraw(bank.GetAccount(1), 250m);

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
            bank.WithDraw(new DecoratorDebit(bank), 600m);

            //Assert
            Assert.AreEqual(bank.GetAccount(1).GetSaldo(), -100m);
        }
    }
}
