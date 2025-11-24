using System;
using System.Configuration;

namespace DriverLicenseDataAccessLayer
{
    static class DataAccessSettings
    {
        //public static string connectionString = "Server=.;Database=Driver_License;User Id=sa;Password=123456;";
        public static string connectionString = ConfigurationManager.ConnectionStrings["MyDBconnection"].ConnectionString;
    }
}
