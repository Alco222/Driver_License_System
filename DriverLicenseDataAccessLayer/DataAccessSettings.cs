using System;
using System.Configuration;

namespace DriverLicenseDataAccessLayer
{
    static class DataAccessSettings
    {
        //public static string connectionString = "Server=.;Database=Driver_License;User Id=sa;Password=123456;";
        public static string connectionString
        {
            get
            {
                var cs = ConfigurationManager.ConnectionStrings["MyDBconnection"];

                if (cs == null)
                    throw new ConfigurationErrorsException(
                        "Connection string 'MyDBconnection' is missing from App.config");

                return cs.ConnectionString;
            }
        }
    }
}
