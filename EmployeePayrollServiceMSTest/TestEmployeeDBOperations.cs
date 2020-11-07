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
    using System;

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

        /*
        [TestMethod]
        public void TestInsertEmployee()
        {
            // Arrange
            Employee employee = new Employee();
            employee.EmployeeID = 0;
            employee.EmployeeName = "Akshay";
            employee.StartDate = Convert.ToDateTime("2020-01-01");
            employee.Gender = "M";
            employee.PhoneNumber = "8094144527";
            employee.Address = "KZH";
            employee.Department = "SDE";
            employee.BasicPay = 25000;
            employee.Deductions = 0;
            employee.TaxablePay = 0;
            employee.IncomeTax = 0;
            employee.NetPay = 0;
            // Act
            EmployeeDBOperations.InsertEmployeeDetails(employee);
            double actual = EmployeeDBOperations.GetSalary("Akshay");
            double expected = 25000;
            // Assert
            Assert.AreEqual(expected, actual);
        }
        */
    }
}