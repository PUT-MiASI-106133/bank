using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bank;
using Ninject;
using System.Reflection;

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
        public void DI_KIRAddBankTest()
        {
            //Arrange
            CKIR KIR = new CKIR();

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var bank1 = kernel.Get<IBank>();
            var bank2 = kernel.Get<IBank>();


            KIR.AddBank(bank1);
            KIR.AddBank(bank2);

            //Assert
            Assert.IsTrue(KIR.IsBankExist(bank1));
            Assert.IsTrue(KIR.IsBankExist(bank2));
            Assert.IsTrue(bank1.GetType() == typeof(CBank)); 

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

        [TestMethod]
        public void TransferResponsibilityTest()
        {
            //Arrange
            CKIR KIR = new CKIR();
            CBank bank = new CBank(KIR);

            bank.AddCustomer("Ivan", "Pavlov", 1);
            bank.StoreAccount(1, 1);

            bank.AddCustomer("Rafal", "Swierczewsky", 2);
            bank.StoreAccount(2, 2);

            US us = new US();
            ITransferResposibility transferNormal = new CNormalTransfer();
            ITransferResposibility transferBig = new CBigTransfer();
            transferNormal.setNextTransferResp(transferBig);
            transferBig.SetUS(us);

            bank.PayIn(bank.GetAccount(1), 30000m);
            Transfer test = bank.Transfer(bank.GetAccount(1), bank.GetAccount(2), 25000m);
            transferNormal.transfer(test);

            //Assert
            Assert.AreEqual(bank.GetAccount(2).GetSaldo(), 25000m);
            Assert.IsNotNull(bank.GetAccount(1).GetHistory());
            Assert.AreEqual(bank.GetAccount(1).GetSaldo(), 5000m);
            Assert.AreEqual(us.GetTransferList().Count, 1); //Logged in US transferlist, transfer > 20000
        }

        [TestMethod]
        public void NoUSTransferResponsibilityTest()
        {
            //Arrange
            CKIR KIR = new CKIR();
            CBank bank = new CBank(KIR);

            bank.AddCustomer("Ivan", "Pavlov", 1);
            bank.StoreAccount(1, 1);

            bank.AddCustomer("Rafal", "Swierczewsky", 2);
            bank.StoreAccount(2, 2);

            US us = new US();
            ITransferResposibility transferNormal = new CNormalTransfer();
            ITransferResposibility transferBig = new CBigTransfer();
            transferNormal.setNextTransferResp(transferBig);
            transferBig.SetUS(us);

            bank.PayIn(bank.GetAccount(1), 30000m);
            Transfer test = bank.Transfer(bank.GetAccount(1), bank.GetAccount(2), 2500m);
            transferNormal.transfer(test);

            //Assert
            Assert.AreEqual(bank.GetAccount(2).GetSaldo(), 2500m);
            Assert.IsNotNull(bank.GetAccount(1).GetHistory());
            Assert.AreEqual(bank.GetAccount(1).GetSaldo(), 27500m);
            Assert.AreEqual(us.GetTransferList().Count, 0); //not logged in US, transfer < 20000
        }

        [TestMethod]
        public void PayInRaportTest()
        {
            //Arrange
            CKIR KIR = new CKIR();
            CBank bank = new CBank(KIR);

            bank.AddCustomer("Ivan", "Pavlov", 1);
            bank.StoreAccount(1, 1);

            bank.PayIn(bank.GetAccount(1), 500m);   //PAYIN
            bank.PayIn(bank.GetAccount(1), 500m);   //PAYIN
            bank.WithDraw(bank, bank.GetAccount(1), 500m);
            bank.PayIn(bank.GetAccount(1), 500m);   //PAYIN
            bank.PayIn(bank.GetAccount(1), 500m);   //PAYIN
            bank.WithDraw(bank, bank.GetAccount(1), 500m);

            IRaport payinraport = new CPayInRaport();
            string temp = payinraport.accept(new CRaportDisplayVisitor(), bank.GetAccount(1).GetHistory());
            string expected = "500-PAYIN...500-PAYIN...500-PAYIN...500-PAYIN";

            //Assert
            Assert.AreEqual(bank.GetAccount(1).GetSaldo(), 1000m);
            Assert.AreEqual(temp, expected);
        }

        [TestMethod]
        public void TransferRaportTest()
        {
            //Arrange
            CKIR KIR = new CKIR();
            CBank bank = new CBank(KIR);

            bank.AddCustomer("Ivan", "Pavlov", 1);
            bank.StoreAccount(1, 1);

            bank.AddCustomer("Rafal", "Swierczewsky", 2);
            bank.StoreAccount(2, 2);

            bank.PayIn(bank.GetAccount(1), 600m);
            bank.Transfer(bank.GetAccount(1), bank.GetAccount(2), 500m);    //TRANSFER
            bank.PayIn(bank.GetAccount(1), 500m);
            bank.PayIn(bank.GetAccount(1), 500m);
            bank.Transfer(bank.GetAccount(1), bank.GetAccount(2), 500m);    //TRANSFER
            bank.WithDraw(bank, bank.GetAccount(1), 500m);
            bank.PayIn(bank.GetAccount(1), 500m);
            bank.PayIn(bank.GetAccount(1), 500m);
            bank.WithDraw(bank, bank.GetAccount(1), 500m);

            IRaport transferraport = new CTransferRaport();
            string temp = transferraport.accept(new CRaportDisplayVisitor(), bank.GetAccount(1).GetHistory());
            string expected = "500-TRANSFER...500-TRANSFER";

            //Assert
            Assert.AreEqual(temp, expected);
        }
        
    }
}
