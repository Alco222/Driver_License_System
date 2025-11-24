using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Utilities;

namespace DriverLicenseDataAccessLayer
{
    public class clsPersonData
    {
        public static int AddNewPerson(string FirstName, string SecondName, string ThirdName,
            string LastName, string NationalNo, byte Gender, string Email, string Address,
            string Phone, DateTime BirthDate, int NationalityID, string ImagePersonel)
        {
            int PersonID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
              
                using (SqlCommand command = new SqlCommand("People.SP_AddNewPerson_DVLD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@FirstName", FirstName);

                    if (!string.IsNullOrEmpty(SecondName))
                        command.Parameters.AddWithValue("@SecondName", SecondName);
                    else
                        command.Parameters.AddWithValue("@SecondName", DBNull.Value);

                    if (!string.IsNullOrEmpty(ThirdName))
                        command.Parameters.AddWithValue("@ThirdName", ThirdName);
                    else
                        command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@Gender", Gender);

                    if (!string.IsNullOrEmpty(Email))
                        command.Parameters.AddWithValue("@Email", Email);
                    else
                        command.Parameters.AddWithValue("@Email", DBNull.Value);

                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@BirthDate", BirthDate);
                    command.Parameters.AddWithValue("@NationalityID", NationalityID);

                    if (!string.IsNullOrEmpty(ImagePersonel))
                        command.Parameters.AddWithValue("@ImagePersonel", ImagePersonel);
                    else
                        command.Parameters.AddWithValue("@ImagePersonel", DBNull.Value);

                    // Create a new SQL parameter named @NewPersonID with type Int
                    // This parameter will be used to get (output) the new ID from the database
                    SqlParameter outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output //Set this parameter as Output type
                    };
                    // Add this parameter to the SQL Command 
                    command.Parameters.Add(outputIdParam);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        object newPersonID = (int)command.Parameters["@NewPersonID"].Value;
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
            return PersonID;
        }

        public static bool UpdatePerson(int PersonID, string FirstName, string SecondName, string ThirdName,
            string LastName, string NationalNo, byte Gender, string Email, string Address,
            string Phone, DateTime BirthDate, int NationalityID, string ImagePersonel)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))

                using (SqlCommand command = new SqlCommand("People.SP_UpdatePerson_DVLD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = FirstName;
                    command.Parameters.Add("@SecondName", SqlDbType.NVarChar, 50).Value = (object)SecondName ?? DBNull.Value;
                    command.Parameters.Add("@ThirdName", SqlDbType.NVarChar, 50).Value = (object)ThirdName ?? DBNull.Value;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = LastName;
                    command.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 20).Value = NationalNo;
                    command.Parameters.Add("@Gender", SqlDbType.TinyInt).Value = Gender;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = (object)Email ?? DBNull.Value;
                    command.Parameters.Add("@Address", SqlDbType.NVarChar, 200).Value = Address;
                    command.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = Phone;
                    command.Parameters.Add("@BirthDate", SqlDbType.Date).Value = BirthDate;
                    command.Parameters.Add("@NationalityID", SqlDbType.Int).Value = NationalityID;
                    command.Parameters.Add("@ImagePersonel", SqlDbType.NVarChar, -1).Value = (object)ImagePersonel ?? DBNull.Value;

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
                return rowsAffected > 0;
        }

        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                //string query = "DELETE FROM People WHERE PersonID = @PersonID";

                using (SqlCommand command = new SqlCommand("People.SP_DeletPerson_DVLD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static DataTable GetAllPerson()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                using (SqlCommand command = new SqlCommand("People.SP_GetAllPerson_DVLD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

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

        public static bool GetPersonByID(int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName,
            ref string LastName, ref string NationalNo, ref byte Gender, ref string Email, ref string Address,
            ref string Phone, ref DateTime BirthDate, ref int NationalityID, ref string ImagePersonel)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))

            //string query = "Select * From People Where PersonID = @PersonID";

            using (SqlCommand command = new SqlCommand("People.SP_GetPersonByID_DVLD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            IsFound = true;
                            FirstName = (string)reader["FirstName"];

                            if (reader["SecondName"] != DBNull.Value)
                            {
                                SecondName = (string)reader["SecondName"];
                            }
                            else
                            {
                                SecondName = "";
                            }

                            if (reader["ThirdName"] != DBNull.Value)
                            {
                                ThirdName = (string)reader["ThirdName"];
                            }
                            else
                            {
                                ThirdName = "";
                            }

                            LastName = (string)reader["LastName"];
                            NationalNo = (string)reader["NationalNo"];
                            Gender = (byte)reader["Gender"];

                            if (reader["Email"] != DBNull.Value)
                            {
                                Email = (string)reader["Email"];
                            }
                            else
                            {
                                Email = "";
                            }

                            Address = (string)reader["Address"];
                            Phone = (string)reader["Phone"];
                            BirthDate = (DateTime)reader["BirthDate"];
                            NationalityID = (int)reader["NationalityID"];

                            if (reader["ImagePersonel"] != DBNull.Value)
                            {
                                ImagePersonel = (string)reader["ImagePersonel"];
                            }
                            else
                            {
                                ImagePersonel = "";
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
            return IsFound;
        }

        public static bool GetPersonByNationalNo(ref int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName,
           ref string LastName, string NationalNo, ref byte Gender, ref string Email, ref string Address,
           ref string Phone, ref DateTime BirthDate, ref int NationalityID, ref string ImagePersonel)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "Select * From People Where NationalNo = @NationalNo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                PersonID = (int)reader["PersonID"];
                                FirstName = (string)reader["FirstName"];

                                if (reader["SecondName"] != DBNull.Value)
                                {
                                    SecondName = (string)reader["SecondName"];
                                }
                                else
                                {
                                    SecondName = "";
                                }

                                if (reader["ThirdName"] != DBNull.Value)
                                {
                                    ThirdName = (string)reader["ThirdName"];
                                }
                                else
                                {
                                    ThirdName = "";
                                }

                                LastName = (string)reader["LastName"];
                                Gender = (byte)reader["Gender"];

                                if (reader["Email"] != DBNull.Value)
                                {
                                    Email = (string)reader["Email"];
                                }
                                else
                                {
                                    Email = "";
                                }

                                Address = (string)reader["Address"];
                                Phone = (string)reader["Phone"];
                                BirthDate = (DateTime)reader["BirthDate"];
                                NationalityID = (int)reader["NationalityID"];

                                if (reader["ImagePersonel"] != DBNull.Value)
                                {
                                    ImagePersonel = (string)reader["ImagePersonel"];
                                }
                                else
                                {
                                    ImagePersonel = "";
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

        public static bool IsPesonExists(string NationalNo)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "Select Found =1 From People Where NationalNo = @NationalNo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);

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

        public static bool IsPesonExists(int PersonID)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            using (SqlCommand command = new SqlCommand("People.SP_CheckPersonExists_DVLD", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;

                // Parameter to capture RETURN value
                var returnParam = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParam.Direction = ParameterDirection.ReturnValue;

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    int ret = (returnParam.Value == DBNull.Value) ? -1 : Convert.ToInt32(returnParam.Value);
                    return ret == 1;

                }
                catch (Exception ex)
                {
                    //Handler Exception Errors.
                    LogException.logException(ex.Message, EventLogEntryType.Error);
                    return false;
                }
            }
        }

        public static int GetAgePerson(int PersonID, ref int Age)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString))
            {
                string query = "Select DATEDIFF(YEAR, BirthDate, GETDATE()) as Age From People Where PersonID = @PersonID";

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
                                Age = reader.GetInt32(0);
                                return Age;
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
            return Age;
        }
    }
}
