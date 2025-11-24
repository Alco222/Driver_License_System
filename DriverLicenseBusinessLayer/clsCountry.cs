using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverLicenseDataAccessLayer;
using static DriverLicenseBusinessLayer.clsPerson;

namespace DriverLicenseBusinessLayer
{
    public class clsCountry
    {
        public int ID { set; get; }
        public string CountryName { set; get; }

        private clsCountry(int ID, string CountryName)

        {
            this.ID = ID;
            this.CountryName = CountryName;

        }

        public static clsCountry Find(int NationalityID)
        {

            string CountryName = "";
           
            if (!clsCountryData.GetCountryInfoByID(NationalityID, ref CountryName))
                return null;
            else

                return new clsCountry(NationalityID, CountryName);

        }

        public static clsCountry Find(string CountryName)
        {

            int NationalityID = -1;
            

            if (clsCountryData.GetCountryInfoByName(CountryName, ref NationalityID))

                return new clsCountry(NationalityID, CountryName);
            else
                return null;

        }


        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();

        }
    }
}
