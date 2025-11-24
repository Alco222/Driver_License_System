using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DriverLicenseDataAccessLayer
{
    public class clsDetainedLicenseData
    {
        public static DataTable GetAllDetainLicense()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "SELECT * FROM DetainLicense_View";

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

        public static int AddNewDetain(int LicenseID, DateTime DetainDate,
            int CreatedByUserID, float FineFees)
        {
            int DetainID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"INSERT INTO DetainedLicenses (LicenseID, DetainDate, FineFees,CreatedByUserID,IsReleased)
                             VALUES (@LicenseID, @DetainDate, @FineFees,@CreatedByUserID,0);
                             SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);
                    command.Parameters.AddWithValue("@DetainDate", DetainDate);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@FineFees", FineFees);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            DetainID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return DetainID;
        }

        public static bool UpdateDetain(int DetainID, int LicenseID, DateTime DetainDate, int CreatedByUserID,
            float FineFees)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"UPDATE DetainedLicenses 
                             SET LicenseID = @LicenseID,
                                 DetainDate = @DetainDate,
                                 FineFees = @FineFees,
                                 CreatedByUserID = @CreatedByUserID
                             WHERE DetainLicenseID = @DetainLicenseID ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);
                    command.Parameters.AddWithValue("@DetainDate", DetainDate);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@FineFees", FineFees);
                    command.Parameters.AddWithValue("@DetainLicenseID", DetainID);

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
            return (rowsAffected > 0);
        }

        public static bool GetDetainLicenseByDetainID(int DetainID, ref int LicenseID, ref int CreatedByUserID, ref DateTime DetainDate,
        ref float FineFees, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"SELECT * FROM DetainedLicenses WHERE DetainLicenseID = @DetainLicenseID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DetainLicenseID", DetainID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                LicenseID = Convert.ToInt32(reader["LicenseID"]);
                                CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                                DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                                FineFees = Convert.ToSingle(reader["FineFees"]);
                                IsReleased = Convert.ToBoolean(reader["IsReleased"]);

                                if (reader["ReleaseDate"] == DBNull.Value)
                                {
                                    ReleaseDate = DateTime.MaxValue;
                                }
                                else
                                {
                                    ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]);
                                }

                                if (reader["ReleasedByUserID"] == DBNull.Value)
                                {
                                    ReleasedByUserID = -1;
                                }
                                else
                                {
                                    ReleasedByUserID = Convert.ToInt32(reader["ReleasedByUserID"]);
                                }

                                if (reader["ReleaseApplicationID"] == DBNull.Value)
                                {
                                    ReleaseApplicationID = -1;
                                }
                                else
                                {
                                    ReleaseApplicationID = Convert.ToInt32(reader["ReleaseApplicationID"]);
                                }
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

        public static bool GetDetainLicenseByLicenseID(ref int DetainID, int LicenseID, ref int CreatedByUserID, ref DateTime DetainDate,
           ref float FineFees, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"SELECT top 1  * FROM DetainedLicenses WHERE LicenseID = @LicenseID order by DetainedLicenseID desc";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                DetainID = Convert.ToInt32(reader["DetainedLicenseID"]);
                                CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                                DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                                FineFees = Convert.ToSingle(reader["FineFees"]);
                                IsReleased = Convert.ToBoolean(reader["IsReleased"]);

                                if (reader["ReleaseDate"] == DBNull.Value)
                                {
                                    ReleaseDate = DateTime.MaxValue;
                                }
                                else
                                {
                                    ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]);
                                }

                                if (reader["ReleaseByUserID"] == DBNull.Value)
                                {
                                    ReleasedByUserID = -1;
                                }
                                else
                                {
                                    ReleasedByUserID = Convert.ToInt32(reader["ReleaseByUserID"]);
                                }

                                if (reader["ReleaseApplicationID"] == DBNull.Value)
                                {
                                    ReleaseApplicationID = -1;
                                }
                                else
                                {
                                    ReleaseApplicationID = Convert.ToInt32(reader["ReleaseApplicationID"]);
                                }
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

        public static bool IsDetainLicense(int LicenseID)
        {
            bool IsDetain = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"SELECT IsDetain = 1 
                             From DetainedLicenses 
                             where LicenseID = @LicenseID 
                             And IsReleased = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsDetain = true;
                            }
                            else
                            {
                                IsDetain = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Handler Exception Errors.
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        IsDetain = false;
                    }
                }
            }
            return IsDetain;
        }

        public static bool ReleaseDetainLicense(int DetainID, int ReleaseByUserID, int ReleaseApplicationID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"UPDATE DetainedLicenses 
                             SET IsReleased = 1, 
                                 ReleaseDate = @ReleaseDate, 
                                 ReleaseByUserID = @ReleaseByUserID, 
                                 ReleaseApplicationID = @ReleaseApplicationID
                             WHERE DetainedLicenseID = @DetainedLicenseID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DetainedLicenseID", DetainID);
                    command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
                    command.Parameters.AddWithValue("@ReleaseByUserID", ReleaseByUserID);
                    command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

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
            return (rowsAffected > 0);
        }
    }
}
