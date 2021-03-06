﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Akshay Poriya"/>
// --------------------------------------------------------------------------------------------------------------------
namespace EmployeePayrollProblem
{
    using System;
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            EmployeeDBOperations.DisplayEmployeeDetails(EmployeeDBOperations.GetAllEmployeeDetails());
            EmployeeDBOperations.GetSalaryStatsGenderWise();
        }
    }
}
