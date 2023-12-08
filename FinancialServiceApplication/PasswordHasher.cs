using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace FinancialServiceApplication
{
    /* This class handles the password for signing up and logging in.
     * The HashPassword() method hashes a password before sending it to the database
     * The VerifyPassword() method compare the entered password with the stored hash in the database
     * I looked into hashing a password for extra user security and protection.
     * This is a CHATGPT generated code which i further implemented to suit the flow of my application. */
    internal class PasswordHasher
    {
        // Generate a salted hash for a password
        public static string HashPassword(string password)
        {
            // Generate a random salt and hash the password
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Verify a password against a stored hash
        public static bool VerifyPassword(string password, string storedHash)
        {
            // Compare the entered password with the stored hash
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}
