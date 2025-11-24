using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;
using System.Dynamic;
using Utilities;
using System.Diagnostics;

namespace DriverLicenseDataAccessLayer
{
    public class clsApplicationData
    {
        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, int AppTypeID,
             byte ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                using (SqlCommand command = new SqlCommand("AppDVL.SP_AddApplication_DVLD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("ApplicantPersonID", ApplicantPersonID);
                    command.Parameters.AddWithValue("ApplicationDate", ApplicationDate);
                    command.Parameters.AddWithValue("AppTypeID", AppTypeID);
                    command.Parameters.AddWithValue("ApplicationStatus", ApplicationStatus);
                    command.Parameters.AddWithValue("LastStatusDate", LastStatusDate);
                    command.Parameters.AddWithValue("PaidFees", PaidFees);
                    command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);

                    // Create a new SQL parameter named @NewAppID with type Int
                    // This parameter will be used to get (output) the new ID from the database
                    SqlParameter outputIdParam = new SqlParameter("@NewAppID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output //Set this parameter as Output type
                    };
                    // Add this parameter to the SQL Command 
                    command.Parameters.Add(outputIdParam);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        object newAppID = (int)command.Parameters["@NewAppID"].Value;
                        if (newAppID != DBNull.Value)
                        {
                            return Convert.ToInt32(newAppID);
                        }
                        else
                        {
                            return -1; // Indicate failure
                        }
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return ApplicationID;
        }

        public static bool UpdateApplication(int ApplicationID, int PersonID, DateTime DateApp, int AppTypeID, byte StatueApp,
           DateTime LastStatueDate, float FeesApp, int CreatedByUserID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"Update ApplicationDVL
                              set  ApplicationDate = @DateApp,
                                   ApplicationStatue = @StatueApp,
                                   ApplicantPersonID = @PersonID,
                                   AppTypeID = @AppTypeID,
                                   ApplicationFees = @FeesApp,
                                   LastStatueApplicationDate = @LastStatueDate,
                                   CreatedByUserID = @CreatedByUserID
                              where ApplicationID = @ApplicationID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DateApp", DateApp);
                    command.Parameters.AddWithValue("@StatueApp", StatueApp);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@AppTypeID", AppTypeID);
                    command.Parameters.AddWithValue("@FeesApp", FeesApp);
                    command.Parameters.AddWithValue("@LastStatueDate", LastStatueDate);
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        return false;
                    }
                }
            }
            return rowsAffected > 0;
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "DELETE FROM ApplicationDVL WHERE ApplicationID = @ApplicationID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        return false;
                    }
                }
            }
            return rowsAffected > 0;
        }

        public static DataTable GetAllApplication()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string Query = @"select * From LocalDrivingLicenseApplications_View";

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
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return dt;
        }

        public static bool GetApplicationByID(int ApplicationID,
            ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID,
            ref byte ApplicationStatus, ref DateTime LastStatusDate,
            ref float PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "SELECT * FROM ApplicationDVL WHERE ApplicationID = @ApplicationID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                ApplicantPersonID = (int)reader["ApplicantPersonID"];
                                ApplicationDate = (DateTime)reader["ApplicationDate"];
                                ApplicationTypeID = (int)reader["AppTypeID"];
                                ApplicationStatus = (byte)reader["ApplicationStatue"];
                                LastStatusDate = (DateTime)reader["LastStatueApplicationDate"];
                                PaidFees = Convert.ToSingle(reader["ApplicationFees"]);
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                            }
                            else
                            {
                                LogException.logException("this Application is not found.", EventLogEntryType.Error);
                                isFound = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        isFound = false;
                    }
                }
            }
            return isFound;
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "SELECT Found=1 FROM ApplicationDVL WHERE ApplicationID = @ApplicationID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = reader.HasRows;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        isFound = false;
                    }
                }
            }
            return isFound;
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);
        }

        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "SELECT ActiveApplicationID=ApplicationID FROM ApplicationDVL WHERE ApplicantPersonID = @ApplicantPersonID and ApplicationTypeID=@ApplicationTypeID and ApplicationStatus=1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int AppID))
                        {
                            ActiveApplicationID = AppID;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return ActiveApplicationID;
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"SELECT ActiveApplicationID=ApplicationDVL.ApplicationID  
                            From
                            ApplicationDVL INNER JOIN
                            LocalDrivingLicenseApplications ON ApplicationDVL.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and AppTypeID=@ApplicationTypeID 
                            and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatue=1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int AppID))
                        {
                            ActiveApplicationID = AppID;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return ActiveApplicationID;
        }

        public static bool UpdateStatus(int ApplicationID, short NewStatus)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"Update  ApplicationDVL  
                            set 
                                ApplicationStatue = @NewStatus, 
                                LastStatueApplicationDate = @LastStatusDate
                            where ApplicationID=@ApplicationID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@NewStatus", NewStatus);
                    command.Parameters.AddWithValue("LastStatusDate", DateTime.Now);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        return false;
                    }
                }
            }
            return (rowsAffected > 0);
        }

        public static bool GetCountPassedTest(int LocalDrivingLicenseApplicationID)
        {
            int CountPassedTest = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = @"SELECT COUNT(*) 
                              FROM dbo.Tests 
                              INNER JOIN dbo.TestAppointments 
                                  ON dbo.Tests.TestAppointmentID = dbo.TestAppointments.TestAppointmentID
                              INNER JOIN LocalDrivingLicenseApplications L
                                  ON dbo.TestAppointments.LocalDrivingLicenseApplicationID = L.LocalDrivingLicenseApplicationID
                              INNER JOIN ApplicationDVL A
                                  ON L.ApplicationID = A.ApplicationID
                              WHERE L.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                                AND A.ApplicationStatue = 1 
                                AND dbo.Tests.TestResult = 1;
                          ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                    try
                    {
                        connection.Open();
                        CountPassedTest = (int)command.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return CountPassedTest == 3;
        }
    }
}
