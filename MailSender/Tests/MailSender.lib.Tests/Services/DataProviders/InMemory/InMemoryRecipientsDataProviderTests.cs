using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MailSender.lib.Services.DataProviders.InMemory;
using MailSender.lib.Entities;

namespace MailSender.lib.Tests.Services.DataProviders.InMemory
{
    /// <summary>
    /// Сводное описание для InMemoryRecipientsDataProviderTests
    /// </summary>
    [TestClass]
    public class InMemoryRecipientsDataProviderTests
    {
        public InMemoryRecipientsDataProviderTests()
        {
            //
            // TODO: добавьте здесь логику конструктора
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты тестирования

        // Первым
        [AssemblyInitialize]
        public static void TestAssembly_Initialize(TestContext context)
        {

        }

        // Перед конструктором класса теста
        [ClassInitialize]
        public static void TestClass_Initialize(TestContext context)
        {

        }

        // Перед выполнением теста
        [TestInitialize]
        public void Test_Initialize()
        {

        }

        // После выполнением теста
        [TestCleanup]
        public void Test_Celanup()
        {

        }

        // После TestCleanup
        [ClassCleanup]
        public static void TestClass_Cleanup()
        {

        }

        // После ClassCleanup
        [AssemblyCleanup]
        public static void TestAssembly_Cleanup()
        {

        }

        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CreateNewRecipientInEmptyProvider()
        {
            var expected_recipient_name = "Тест Получатель";
            var expected_recipient_address = "recipient_test@server.com";
            var expected_recipient_id = 1;

            var data_provider = new InMemoryRecipientsDataProvider();
            var new_recipient = new Recipient
            {
                Name = expected_recipient_name,
                Address = expected_recipient_address
            };
            
            data_provider.Create(new_recipient);

            var actual_recipient_id = new_recipient.Id;
            var actual_recipient = data_provider.GetById(actual_recipient_id);

            Assert.AreEqual(expected_recipient_id, actual_recipient_id);
            Assert.AreEqual(expected_recipient_name, actual_recipient.Name);
            Assert.AreEqual(expected_recipient_address, actual_recipient.Address);

            // Work with strings
            // StringAssert.Matches("value string", new System.Text.RegularExpressions.Regex(@"\w+\s\w+"));

            // Work with collection
            // CollectionAssert

            // Handed Exception
            if (expected_recipient_id != actual_recipient_id)
                throw new AssertFailedException("Идентификаторы объектов не совпадают");

        }
    }
}
