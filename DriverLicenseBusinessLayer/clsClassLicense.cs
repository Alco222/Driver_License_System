using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverLicenseDataAccessLayer;

namespace DriverLicenseBusinessLayer
{
    public class clsClassLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
       
        public int LicenseClassID { set; get; }
        public string Name { set; get; }
        public string Descriptions { set; get; }
        public int MinimuuAllwoedAge { set; get; }
        public byte ValidityLenghtYear { set; get; }
        public float FessClass { set; get; }

        public clsClassLicense()
        {
            this.LicenseClassID = -1;
            this.Name = "";
            this.Descriptions = "";
            this.MinimuuAllwoedAge = 0;
            this.ValidityLenghtYear = 0;
            this.FessClass = 0;
            Mode = enMode.AddNew;
        }

        private clsClassLicense(int LicenseClassID, string Name, string Descriptions, int MinimuuAllwoedAge,
            byte ValidityLenghtYear, float FessClass)
        {
            this.LicenseClassID = LicenseClassID;
            this.Name = Name;
            this.Descriptions = Descriptions;
            this.MinimuuAllwoedAge = MinimuuAllwoedAge;
            this.ValidityLenghtYear = ValidityLenghtYear;
            this.FessClass = FessClass;
            Mode = enMode.Update;
        }

        public static clsClassLicense Find(int LicenseClassID)
        {

            string Name = "",Descriptions = "";
            byte MinimuuAllwoedAge = 0, ValidityLenghtYear = 0;
            float FessClass = 0;

            if (!clsClassLicenseData.GetLicenseClassInfoByID(LicenseClassID, ref Name,ref Descriptions,ref MinimuuAllwoedAge,
                ref ValidityLenghtYear,ref FessClass))
                return null;
            else

                return new clsClassLicense(LicenseClassID,Name,Descriptions,MinimuuAllwoedAge,ValidityLenghtYear,FessClass);

        }

        public static clsClassLicense Find(string Name)
        {
            int LicenseClassID = -1;
            string Descriptions = "";
            byte MinimuuAllwoedAge = 0, ValidityLenghtYear = 0;
            float FessClass = 0;

            if (!clsClassLicenseData.GetLicenseClassInfoByClassName(Name,ref LicenseClassID, ref Descriptions, ref MinimuuAllwoedAge,
                ref ValidityLenghtYear, ref FessClass))
                return null;
            else

                return new clsClassLicense(LicenseClassID,Name, Descriptions, MinimuuAllwoedAge, ValidityLenghtYear, FessClass);


        }

        public static DataTable GetAllClassLicense()
        {
            return clsClassLicenseData.GetAllLicenseClasses();

        }

        public static bool DoesPersonAgeMatchedTehMinimumAgeInclass(int LicenseClassID, int PersonAge)
        {
            int MinimumAllwoedAge = Find(LicenseClassID).MinimuuAllwoedAge;

            if (PersonAge >= MinimumAllwoedAge)
            {
                return true;
            }
            return false;
        }

    }
}
