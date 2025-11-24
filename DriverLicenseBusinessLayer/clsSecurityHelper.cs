using System;
using System.Security.Cryptography;
using System.Text;

namespace DriverLicenseBusinessLayer
{
    public static class clsSecurityHelper
    {
        public static string ComputeHash(string input)
        {
            /*The SHA-256 hash output is represented as a 64-character hexadecimal string 
              because each character in a hexadecimal representation represents 4 bits. 
              Since SHA-256 produces a 256-bit hash value, 
              the hexadecimal representation requires 256 bits / 4 bits per character = 64 characters
             */

            //SHA-256(Securty Hash Algorithem - 256 bit).
            //Create an instance of the SHA-256 algorithem.
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));


                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }

        }
    }
}
