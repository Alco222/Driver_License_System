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
    public class clsInternationalLicenseData
    {
        public static bool GetInternationalLicenseByID(int InternationalLicenseID, ref int ApplicationID, ref int DriverID, ref int IssuedUsedLocalLicenseID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int createdByUserID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"SELECT * From InternationaLicenses where InternationalLicenseID = @InternationalLicenseID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                                DriverID = Convert.ToInt32(reader["DriverID"]);
                                IssuedUsedLocalLicenseID = Convert.ToInt32(reader["IssuedUsedLocalLicenseID"]);
                                IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                                ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                                IsActive = Convert.ToBoolean(reader["IsActive"]);
                                createdByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
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

        public static int AddNewInternationalLicense(int ApplicationID, int DriverID, int IssuedUsedLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"INSERT INTO InternationaLicenses (ApplicationID,DriverID,IssuedUsedLocalLicenseID,
                                       IssueDate,ExpirationDate,IsActive,CreatedByUserID)
                               VALUES (@ApplicationID,@DriverID,@IssuedUsedLocalLicenseID,
                                       @IssueDate,@ExpirationDate,@IsActive,@CreatedByUserID);
                               SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@DriverID", DriverID);
                    command.Parameters.AddWithValue("@IssuedUsedLocalLicenseID", IssuedUsedLocalLicenseID);
                    command.Parameters.AddWithValue("@IssueDate", IssueDate);
                    command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                    command.Parameters.AddWithValue("@IsActive", IsActive); // تم تصحيح اسم المعامل من @ISActive إلى @IsActive
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            InternationalLicenseID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return InternationalLicenseID;
        }

        public static bool UpdateInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsedLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"Update InternationaLicenses
                              set  ApplicationID = @ApplicationID,
                                   DriverID = @DriverID,
                                   IssuedUsedLocalLicenseID = @IssuedUsedLocalLicenseID,
                                   IssueDate = @IssueDate,
                                   ExpirationDate = @ExpirationDate,
                                   IsActive = @IsActive,
                                   CreatedByUserID = @CreatedByUserID
                                where InternationalLicenseID = @InternationalLicenseID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@DriverID", DriverID);
                    command.Parameters.AddWithValue("@IssuedUsedLocalLicenseID", IssuedUsedLocalLicenseID);
                    command.Parameters.AddWithValue("@IssueDate", IssueDate);
                    command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        public static DataTable GetAllInternationalLicense()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"SELECT InternationalLicenseID,ApplicationID,
                                     DriverID,IssuedUsedLocalLicenseID,
                                     IssueDate,ExpirationDate,IsActive
                             FROM InternationaLicenses
                                   Order by ExpirationDate Desc";

                using (SqlCommand command = new SqlCommand(query, connection))
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

        public static DataTable GetDriverInternationaLicenses(int DriverID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"SELECT IL.InternationalLicenseID,IL.ApplicationID,IL.IssuedUsedLocalLicenseID,
                                     IL.IssueDate,IL.ExpirationDate,IL.IsActive
                              FROM InternationaLicenses IL 
                             WHERE DriverID = @DriverID
                             order By ExpirationDate Desc";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int InternationalLicenseID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"SELECT TOP 1 InternationalLicenseID 
                              FROM InternationaLicenses 
                             WHERE DriverID = @DriverID AND GetDate() between IssueDate AND ExpirationDate
                             Order by ExpirationDate Desc";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", DriverID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertID))
                        {
                            InternationalLicenseID = insertID;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return InternationalLicenseID;
        }

        public static bool DeactivateInternationalLicense(int InternationalLicenseID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"Update InternationaLicenses
                              set IsActive = 0
                                where InternationalLicenseID = @InternationalLicenseID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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
    }
}
