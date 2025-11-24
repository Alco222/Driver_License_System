using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Microsoft.Win32;



namespace DriverLicenseBusinessLayer
{
    /*    public class LoggedInUser
        {
            public delegate Task DelegatLoggedInUser(clsUsers user);

            private static event DelegatLoggedInUser _LoggedInUser;
            //private static string Key = "YU48FRT14EG148HJ";
            private static string Key = "AWF148JMK748NMJ4";


            public LoggedInUser(DelegatLoggedInUser loggedInUser)
            {
                _LoggedInUser = loggedInUser;
            }

            public async Task Logger(clsUsers user)
            {
                if (_LoggedInUser != null)
                    await _LoggedInUser.Invoke(user);
            }

            public static clsUsers CurrentUser;

            private static string Encrypt(string plainText, string Key)
            {
                using (Aes aesAlg = Aes.Create())
                {
                    // Set the key and IV for AES encryption
                    aesAlg.Key = Encoding.UTF8.GetBytes(Key);


                    //Here, you are setting the IV of the AES algorithm to a block of bytes
                    //  with a size equal to the block size of the algorithm divided by 8.
                    //  The block size of AES is typically 128 bits(16 bytes), 
                    //  so the IV size is 128 bits / 8 = 16 bytes.

                    aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                    // Create an encryptor
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Encrypt the data
                    using (var msEncrypt = new System.IO.MemoryStream())
                    {
                        using (var csEnrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new System.IO.StreamWriter(csEnrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                        }
                        // Return the encrypted data as a Base64-encoded string
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }

            private static string Decrypt(string cipherText, string key)
            {
                using (Aes aesAlg = Aes.Create())
                {
                    // Set the key and IV for AES decryption
                    aesAlg.Key = Encoding.UTF8.GetBytes(key);
                    aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                    // Create a decryptor
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Decrypt the data
                    using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                            {
                                // Read the decrypted data from the StreamReader
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }

            public static bool RememberUsernameAndPassword(string Username, string Password)
            {
                string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD-Login-Data";
                string KeyPathDelete = @"SOFTWARE\DVLD-Login-Data";
                string ValueNameUserName = "UserName";
                string ValueNamePassword = "Password";

                try
                {
                    // Open the registry key in read/write mode with explicit registry view
                    using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                    {
                        using (RegistryKey key = baseKey.OpenSubKey(KeyPathDelete, true))
                        {
                            if (key != null)
                            {
                                if (Username == null)
                                {
                                    // Delete the specified value
                                    if (key.GetValue(ValueNameUserName) != null)
                                        key.DeleteValue(ValueNameUserName, false);

                                    if (key.GetValue(ValueNamePassword) != null)
                                        key.DeleteValue(ValueNamePassword, false);

                                    return true;
                                }
                            }
                            //Write the value to the Registry.
                            Registry.SetValue(KeyPath, ValueNamePassword, Encrypt(Password, Key), RegistryValueKind.String);
                            Registry.SetValue(KeyPath, ValueNameUserName, Encrypt(Username, Key), RegistryValueKind.String);
                            return true;
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("UnauthorizedAccessException: Run the program with administrative privileges.");
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                    return false;
                }
            }
            public static bool GetStoredCredential(ref string Username, ref string Password)
            {
                string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD-Login-Data";
                string ValueNameUserName = "UserName";
                string ValueNamePassword = "Password";

                try
                {
                    string ValueUserName = Registry.GetValue(KeyPath, ValueNameUserName, null) as string;
                    string ValuePassword = Registry.GetValue(KeyPath, ValueNamePassword, null) as string;

                    if (ValueUserName != null && ValuePassword != null)
                    {
                        Username = Decrypt(ValueUserName, Key);
                        Password = Decrypt(ValuePassword, Key);
                        return true;
                    }
                    else
                    {
                        Username = string.Empty;
                        Password = string.Empty;
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                    return false;
                }
            }
        }
    */
    public class LoggedInUser
    {
        public delegate void DelegatLoggedInUser(clsUsers user);

        private static event DelegatLoggedInUser _LoggedInUser;
        //private static string Key = "YU48FRT14EG148HJ";
        private static string Key = "AWF148JMK748NMJ4";

        public LoggedInUser(DelegatLoggedInUser loggedInUser)
        {
            _LoggedInUser = loggedInUser;
        }

        public void Logger(clsUsers user)
        {
            if (_LoggedInUser != null)
                _LoggedInUser.Invoke(user);
        }

        public static clsUsers CurrentUser;

        private static string Encrypt(string plainText, string Key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and IV for AES encryption
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);


                //Here, you are setting the IV of the AES algorithm to a block of bytes
                //with a size equal to the block size of the algorithm divided by 8. 
                //The block size of AES is typically 128 bits(16 bytes), 
                //so the IV size is 128 bits / 8 = 16 bytes.

                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                // Create an encryptor
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Encrypt the data
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEnrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new System.IO.StreamWriter(csEnrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    // Return the encrypted data as a Base64-encoded string
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        private static string Decrypt(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and IV for AES decryption
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                // Create a decryptor
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Decrypt the data
                using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                        {
                            // Read the decrypted data from the StreamReader
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD-Login-Data";
            string KeyPathDelete = @"SOFTWARE\DVLD-Login-Data";
            string ValueNameUserName = "UserName";
            string ValueNamePassword = "Password";

            try
            {
                // Open the registry key in read/write mode with explicit registry view
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key = baseKey.OpenSubKey(KeyPathDelete, true))
                    {
                        if (key != null)
                        {
                            if (Username == null)
                            {
                                // Delete the specified value
                                if (key.GetValue(ValueNameUserName) != null)
                                    key.DeleteValue(ValueNameUserName, false);

                                if (key.GetValue(ValueNamePassword) != null)
                                    key.DeleteValue(ValueNamePassword, false);

                                return true;
                            }
                        }
                        //Write the value to the Registry.
                        Registry.SetValue(KeyPath, ValueNamePassword, Encrypt(Password, Key), RegistryValueKind.String);
                        Registry.SetValue(KeyPath, ValueNameUserName, Encrypt(Username, Key), RegistryValueKind.String);
                        return true;
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("UnauthorizedAccessException: Run the program with administrative privileges.");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD-Login-Data";
            string ValueNameUserName = "UserName";
            string ValueNamePassword = "Password";

            try
            {
                string ValueUserName = Registry.GetValue(KeyPath, ValueNameUserName, null) as string;
                string ValuePassword = Registry.GetValue(KeyPath, ValueNamePassword, null) as string;

                if (ValueUserName != null && ValuePassword != null)
                {
                    Username = Decrypt(ValueUserName, Key);
                    Password = Decrypt(ValuePassword, Key);
                    return true;
                }
                else
                {
                    Username = string.Empty;
                    Password = string.Empty;
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }

    }
}
