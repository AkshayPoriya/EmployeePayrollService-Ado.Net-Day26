// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestEmployeeDBOperations.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Akshay Poriya"/>
// --------------------------------------------------------------------------------------------------------------------
namespace EmployeePayrollServiceMSTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EmployeePayrollProblem;
    using System.Security.Cryptography;

    [TestClass]
    public class TestEmployeeDBOperations
    {
        [TestMethod]
        [DataRow("Rachel",50200)]
        [DataRow("Joey",35700)]
        public void TestUpdateSalary(string name, double basicPay)
        {
            // Arrange
            EmployeeDBOperations.UpdateSalary(name, basicPay);
            // Act
            double actual = EmployeeDBOperations.GetSalary(name);
            double expected = basicPay;
            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
