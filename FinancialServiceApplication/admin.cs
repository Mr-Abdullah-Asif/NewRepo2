using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SqlClient;

namespace FinancialServiceApplication
{
    internal class admin
    {
        private List<string> selectedRoles = new List<string>();

        public class UserManagement
        {
            private string connectionString = "your_connection_string_here";

        }

        // Event handler for update button click

        public static void DeleteUser(DBConnection dBConnection, SqlQueries query, int user_id)
        {
            try
            {
                string deleteUserQuery = query.deleteUser(user_id);

                // Use parameters for better security
                ArrayList parameters = new ArrayList();
                parameters.Add(new SqlParameter("user_id", user_id));

                // Call the appropriate method to delete the user from the database
                dBConnection.saveToDatabase(deleteUserQuery, parameters); // Add an empty string or provide an appropriate value for the 'role' parameter
                Console.WriteLine("User deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
            }
        }



    }
}
