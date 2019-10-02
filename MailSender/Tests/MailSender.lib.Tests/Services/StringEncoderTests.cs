using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MailSender.lib.Services;

namespace MailSender.lib.Tests.Services
{
    /// <summary>
    /// Сводное описание для StringEncoderTests
    /// </summary>
    [TestClass]
    public class StringEncoderTests
    {
        public StringEncoderTests()
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
        public void Encode_ABC_to_BCD_with_key_1()
        {
            // Принцип ААА
            // A - Arrange - ввод
            // A - Act - 1 действие
            // A - Assert - проверка

            #region Arrange

            var str = "ABC";
            var expected_result = "BCD";
            var key = 1;

            #endregion

            #region Act

            var actual_result = StringEncoder.Encode(str, key);

            #endregion

            #region Assert

            Assert.AreEqual(expected_result, actual_result);

            #endregion
        }

        [TestMethod]
        public void Decode_BCD_to_ABC_with_key_1()
        {
            // Принцип ААА
            // A - Arrange - ввод
            // A - Act - 1 действие
            // A - Assert - проверка

            #region Arrange

            var str = "BCD";
            var expected_result = "ABC";
            var key = 1;

            #endregion

            #region Act

            var actual_result = StringEncoder.Decode(str, key);

            #endregion

            #region Assert

            Assert.AreEqual(expected_result, actual_result);

            #endregion
        }
    }
}
