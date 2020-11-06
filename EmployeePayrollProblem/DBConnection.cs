using System;
using System.Data.SqlClient;

namespace EmployeePayrollProblem
{
    class DBConnection
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source = AKSHAY; Initial Catalog = payroll_service; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
