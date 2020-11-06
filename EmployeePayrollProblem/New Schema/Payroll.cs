using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayrollProblem.New_Schema
{
    class Payroll
    {
        public int PayrollID { get; set; }
        public double BasicPay { get; set; }
        public double Deductions { get; set; }
        public double TaxablePay { get; set; }
        public double IncomeTax { get; set; }
        public double NetPay { get; set; }
        public int EmployeeID { get; set; }

    }
}
