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
        public static void InsertEmployeeDetails(Employee employee)
        {
            SqlConnection sqlConnection = DBConnection.GetConnection();
            int id = employee.EmployeeID;
            string name = employee.EmployeeName;
            DateTime start = employee.StartDate;
            string gender = employee.Gender;
            string phoneNumber = employee.PhoneNumber;
            string address = employee.Address;
            string department = employee.Department;
            double basicPay = employee.BasicPay;
            double deductions = employee.Deductions;
            double taxablePay = employee.TaxablePay;
            double incomeTax = employee.IncomeTax;
            double netPay = employee.NetPay;
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    string query = @"INSERT INTO dbo.employee_payroll(name,start,gender,phone_number,address,department,basic_pay)
                                     VALUES
                                    (@name,@start,@gender,@phoneNumber,@address,@department,@basicPay)";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@name", name);
                    sqlCommand.Parameters.AddWithValue("@start", start);
                    sqlCommand.Parameters.AddWithValue("@gender", gender);
                    sqlCommand.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    sqlCommand.Parameters.AddWithValue("@address", address);
                    sqlCommand.Parameters.AddWithValue("@department", department);
                    sqlCommand.Parameters.AddWithValue("@basicPay", basicPay);
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
                    string query = @"SELECT e.id, e.name,  e.start, e.gender, e.address, d.dept_name, p.basic_pay, p.deductions, p.taxable_pay, p.income_tax, p.net_pay
                                    FROM employee e, payroll p, department d, employee_department ed
                                    WHERE e.id = p.emp_id AND e.id = ed.emp_id 
		                                    AND ed.dept_id=d.dept_id";
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
                            //string phoneNumber = dataReader["phone_number"].ToString();
                            string address = dataReader["address"].ToString();
                            string department = dataReader["dept_name"].ToString();
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
                            //employee.PhoneNumber = phoneNumber;
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
                    string query = @"update dbo.payroll set basic_pay=@pay 
                                     where emp_id = (select e.id from employee e where name=@name)";
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
                    string query = @"select p.basic_pay from dbo.payroll p 
                                    where p.emp_id = (select e.id from employee e where name=@name)";
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


        public static void GetSalaryStatsGenderWise()
        {
            SqlConnection sqlConnection = DBConnection.GetConnection();
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    string query = @"SELECT e.gender,SUM(p.basic_pay) AS 'salary_sum',AVG(p.basic_pay) AS 'salary_avg',MIN(p.basic_pay) AS 'salary_min',MAX(p.basic_pay) AS 'salary_max',COUNT(p.basic_pay) AS 'person_count' FROM employee e,payroll p WHERE e.id=p p.emp_idGROUP BY e.gender;";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    if (dataReader.HasRows == false)
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Write("{0,-15}", "gender");
                        Console.Write("{0,-15}", "salarySum");
                        Console.Write("{0,-15}", "salaryAvg");
                        Console.Write("{0,-15}", "salaryMin");
                        Console.Write("{0,-15}", "salaryMax");
                        Console.Write("{0,-15}", "personCount");
                        Console.WriteLine();
                        while (dataReader.Read())
                        {
                            string gender = dataReader["gender"].ToString();
                            double salarySum = Convert.ToDouble(dataReader["salary_sum"].ToString());
                            double salaryAvg = Convert.ToDouble(dataReader["salary_avg"].ToString());
                            double salaryMin = Convert.ToDouble(dataReader["salary_min"].ToString());
                            double salaryMax = Convert.ToDouble(dataReader["salary_max"].ToString());
                            int personCount = Convert.ToInt32(dataReader["person_count"].ToString());

                            Console.Write("{0,-15}", gender);
                            Console.Write("{0,-15}", Math.Round(salarySum, 2));
                            Console.Write("{0,-15}", Math.Round(salaryAvg, 2));
                            Console.Write("{0,-15}", Math.Round(salaryMin, 2));
                            Console.Write("{0,-15}", Math.Round(salaryMax, 2));
                            Console.Write("{0,-15}", personCount);
                            Console.WriteLine();
                        }
                        dataReader.Close();
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
