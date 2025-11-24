using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DriverLicenseDataAccessLayer
{
    public class clsDataUsers
    {
        public static int AddNewUser(string UserName, string Password, bool IsActive, int PersonID)
        {
            int UserID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                using (SqlCommand command = new SqlCommand("Users.SP_AddNewUser_DVLD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Passwords", Password);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    // Create a new SQL parameter named @NewPersonID with type Int
                    // This parameter will be used to get (output) the new ID from the database
                    SqlParameter outputIdParam = new SqlParameter("@NewUserID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output //Set this parameter as Output type
                    };
                    // Add this parameter to the SQL Command 
                    command.Parameters.Add(outputIdParam);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        object newPersonID = (int)command.Parameters["@NewUserID"].Value;
                        if (newPersonID != DBNull.Value)
                        {
                            return Convert.ToInt32(newPersonID);
                        }
                        else
                        {
                            return -1; // Indicate failure
                        }
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return UserID;
        }

        public static bool UpdateUser(int UserID, string UserName, string Password, bool IsActive, int PersonID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"Update Users
                              set  UserName = @UserName,
                                   Password = @Password,
                                   IsActive = @IsActive,
                                   PersonID = @PersonID
                              where UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@UserID", UserID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        return false;
                    }
                }
            }
            return rowsAffected > 0;
        }

        public static bool DeleteUser(int UserID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "DELETE FROM Users WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        return false;
                    }
                }
            }
            return rowsAffected > 0;
        }

        public static DataTable GetAllUser()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string Query = @"SELECT
                                US.UserID,US.PersonID,P.FirstName +''+ P.SecondName +''+ P.ThirdName
                                 +''+ P.LastName As FullName, US.UserName,
                                US.IsActive
                              FROM Users US INNER JOIN 
                              People P ON US.PersonID = P.PersonID ";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return dt;
        }

        public static bool GetUserByUserID(int UserID, ref string UserName, ref string Password, ref bool IsActive, ref int PersonID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "Select * From Users Where UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                IsActive = (bool)reader["IsActive"];
                                PersonID = (int)reader["PersonID"];
                            }
                            else
                            {
                                IsFound = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        IsFound = false;
                    }
                }
            }
            return IsFound;
        }

        public static bool GetUserByUserName(ref int UserID, string UserName, ref string Password, ref bool IsActive, ref int PersonID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "Select * From Users Where UserName = @UserName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                UserID = (int)reader["UserID"];
                                Password = (string)reader["Password"];
                                IsActive = (bool)reader["IsActive"];
                                PersonID = (int)reader["PersonID"];
                            }
                            else
                            {
                                IsFound = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        IsFound = false;
                    }
                }
            }
            return IsFound;
        }

        public static bool GetUserByPersonID(ref int UserID, ref string UserName, ref string Password, ref bool IsActive, int PersonID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "Select * From Users Where PersonID = @PersonID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                UserID = (int)reader["UserID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                IsActive = (bool)reader["IsActive"];
                            }
                            else
                            {
                                IsFound = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        IsFound = false;
                    }
                }
            }
            return IsFound;
        }

        public static bool LoginUser(ref int UserID, string UserName, string Password, ref bool IsActive, ref int PersonID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "Select * From Users Where UserName = @UserName And Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@UserName", UserName);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader =  command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                UserID = (int)reader["UserID"];
                                IsActive = (bool)reader["IsActive"];
                                PersonID = (int)reader["PersonID"];
                            }
                            else
                            {
                                IsFound = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        IsFound = false;
                    }
                }
            }
            return IsFound;
        }

        public static async Task<(bool IsFound, int UserID, bool IsActive, int PersonID)> LoginUserAsync(string UserName, string Password)
        {
            int UserID = -1;
            bool IsActive = false;
            int PersonID = -1;
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "Select * From Users Where UserName = @UserName And Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@UserName", UserName);

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                IsFound = true;
                                UserID = (int)reader["UserID"];
                                IsActive = (bool)reader["IsActive"];
                                PersonID = (int)reader["PersonID"];
                            }
                            else
                            {
                                IsFound = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        IsFound = false;
                    }
                }
            }
            return (IsFound,UserID,IsActive,PersonID);
        }

        public static bool IsUserExists(string UserName)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "Select * From Users Where UserName = @UserName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                            }
                            else
                            {
                                IsFound = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        IsFound = false;
                    }
                }
            }
            return IsFound;
        }

        public static bool IsUserExists(int PersonID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "Select Found = 1 From Users Where PersonID = @PersonID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            IsFound = reader.HasRows;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        IsFound = false;
                    }
                }
            }
            return IsFound;
        }

        public static bool IsUserExistsByPersonID(int PersonID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "Select Found = 1 From Users Where PersonID = @PersonID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            IsFound = reader.HasRows;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        IsFound = false;
                    }
                }
            }
            return IsFound;
        }

        public static bool ChangePassword(int UserID, string NewPassword)
        {
            bool rowsAffected = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"Update Users 
                             Set Password = @Password 
                             Where UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Password", NewPassword);
                    command.Parameters.AddWithValue("@UserID", UserID);

                    try
                    {
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        rowsAffected = result > 0;
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        rowsAffected = false;
                    }
                }
            }
            return rowsAffected;
        }
    }
}
