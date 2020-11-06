// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmployeeDBOperations.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Akshay Poriya"/>
// --------------------------------------------------------------------------------------------------------------------
namespace EmployeePayrollProblem
{
    using System;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;

    /// <summary>
    /// 
    /// </summary>
    public class EmployeeDBOperations
    {
        /// <summary>
        /// UC2
        /// </summary>
        /// <returns></returns>
        public static List<Employee> GetAllEmployeeDetails()
        {
            SqlConnection sqlConnection = DBConnection.GetConnection();
            List<Employee> employeeList = new List<Employee>();
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    string query = @"select * from dbo.employee_payroll";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows == false)
                    {
                        return employeeList;
                    }
                    else
                    {
                        while (dataReader.Read())
                        {
                            int id = Convert.ToInt32(dataReader["id"].ToString());
                            string name = dataReader["name"].ToString();
                            DateTime start = Convert.ToDateTime(dataReader["start"].ToString());
                            string gender = dataReader["gender"].ToString();
                            string phoneNumber = dataReader["phone_number"].ToString();
                            string address = dataReader["address"].ToString();
                            string department = dataReader["department"].ToString();
                            double basicPay = Convert.ToDouble(dataReader["basic_pay"].ToString());
                            double deductions = Convert.ToDouble(dataReader["deductions"].ToString());
                            double taxablePay = Convert.ToDouble(dataReader["taxable_pay"].ToString());
                            double incomeTax = Convert.ToDouble(dataReader["income_tax"].ToString());
                            double netPay = Convert.ToDouble(dataReader["net_pay"].ToString());

                            Employee employee = new Employee();
                            employee.EmployeeID = id;
                            employee.EmployeeName = name;
                            employee.StartDate = start;
                            employee.Gender = gender;
                            employee.PhoneNumber = phoneNumber;
                            employee.Address = address;
                            employee.Department = department;
                            employee.BasicPay = basicPay;
                            employee.Deductions = deductions;
                            employee.TaxablePay = taxablePay;
                            employee.IncomeTax = incomeTax;
                            employee.NetPay = netPay;

                            employeeList.Add(employee);
                        }
                        dataReader.Close();
                        return employeeList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        /// <summary>
        /// UC2
        /// </summary>
        /// <param name="employeeList"></param>
        public static void DisplayEmployeeDetails(List<Employee> employeeList)
        {
            Console.WriteLine("Employee Details:");
            Console.Write("{0,-5}", "id");
            Console.Write("{0,-15}", "name");
            Console.Write("{0,-25}", "start");
            Console.Write("{0,-10}", "gender");
            Console.Write("{0,-15}", "phone_number");
            Console.Write("{0,-15}", "address");
            Console.Write("{0,-15}", "department");
            Console.Write("{0,-15}", "basic_pay");
            Console.Write("{0,-15}", "deductions");
            Console.Write("{0,-15}", "taxable_pay");
            Console.Write("{0,-15}", "income_tax");
            Console.Write("{0,-15}", "net_pay");
            Console.WriteLine();
            //List<Employee> employeeList = GetAllEmployeeDetails();
            foreach (Employee employee in employeeList)
            {
                Console.Write("{0,-5}", employee.EmployeeID);
                Console.Write("{0,-15}", employee.EmployeeName);
                Console.Write("{0,-25}", employee.StartDate);
                Console.Write("{0,-10}", employee.Gender);
                Console.Write("{0,-15}", employee.PhoneNumber);
                Console.Write("{0,-15}", employee.Address);
                Console.Write("{0,-15}", employee.Department);
                Console.Write("{0,-15}", employee.BasicPay);
                Console.Write("{0,-15}", employee.Deductions);
                Console.Write("{0,-15}", employee.TaxablePay);
                Console.Write("{0,-15}", employee.IncomeTax);
                Console.Write("{0,-15}", employee.NetPay);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// UC3 and UC4
        /// This function is equivalent to prepared statement in C#
        /// </summary>
        /// <param name="name"></param>
        /// <param name="basicPay"></param>
        public static void UpdateSalary(string name, double basicPay)
        {
            SqlConnection sqlConnection = DBConnection.GetConnection();
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    string query = @"update dbo.employee_payroll set basic_pay=@pay where name=@name";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@pay", basicPay);
                    sqlCommand.Parameters.AddWithValue("@name", name);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        /// <summary>
        /// UC3
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static double GetSalary(string name)
        {
            SqlConnection sqlConnection = DBConnection.GetConnection();
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    string query = @"select basic_pay from dbo.employee_payroll where name=@name";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@name", name);
                    double basicPay = (double)sqlCommand.ExecuteScalar();
                    return basicPay;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        /// <summary>
        /// UC5
        /// </summary>
        /// <returns></returns>
        public static List<Employee> GetEmployeeDetailsWithinStartDates(string startDate, string endDate)
        {
            SqlConnection sqlConnection = DBConnection.GetConnection();
            List<Employee> employeeList = new List<Employee>();
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    string query = @"SELECT * FROM dbo.employee_payroll WHERE start BETWEEN CAST(@startDate AS DATE) AND CAST(@endDate AS DATE) ";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    DateTime start_date = Convert.ToDateTime(startDate);
                    DateTime end_date = Convert.ToDateTime(endDate);
                    sqlCommand.Parameters.AddWithValue("@startDate", start_date);
                    sqlCommand.Parameters.AddWithValue("@endDate", end_date);
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows == false)
                    {
                        return employeeList;
                    }
                    else
                    {
                        while (dataReader.Read())
                        {
                            int id = Convert.ToInt32(dataReader["id"].ToString());
                            string name = dataReader["name"].ToString();
                            DateTime start = Convert.ToDateTime(dataReader["start"].ToString());
                            string gender = dataReader["gender"].ToString();
                            string phoneNumber = dataReader["phone_number"].ToString();
                            string address = dataReader["address"].ToString();
                            string department = dataReader["department"].ToString();
                            double basicPay = Convert.ToDouble(dataReader["basic_pay"].ToString());
                            double deductions = Convert.ToDouble(dataReader["deductions"].ToString());
                            double taxablePay = Convert.ToDouble(dataReader["taxable_pay"].ToString());
                            double incomeTax = Convert.ToDouble(dataReader["income_tax"].ToString());
                            double netPay = Convert.ToDouble(dataReader["net_pay"].ToString());

                            Employee employee = new Employee();
                            employee.EmployeeID = id;
                            employee.EmployeeName = name;
                            employee.StartDate = start;
                            employee.Gender = gender;
                            employee.PhoneNumber = phoneNumber;
                            employee.Address = address;
                            employee.Department = department;
                            employee.BasicPay = basicPay;
                            employee.Deductions = deductions;
                            employee.TaxablePay = taxablePay;
                            employee.IncomeTax = incomeTax;
                            employee.NetPay = netPay;

                            employeeList.Add(employee);
                        }
                        dataReader.Close();
                        return employeeList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
