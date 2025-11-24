using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverLicenseDataAccessLayer;

namespace DriverLicenseBusinessLayer
{
    public class clsDetainedLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int DetainID { set; get; }
        public int LicenseID { set; get; }
        public int CreatedByUserID { set; get; }

        public clsUsers CreatedByUserInfo { get; set; }
        public DateTime DetainDate { get; set; }

        public float FineFees { get; set; }

        public DateTime ReleaseDate { get; set; }

        public bool IsReleased { get; set; }

        public int ReleasedByUserID { get; set; }

        public clsUsers ReleasedByUserInfo { get; set; }
        public int ReleaseApplicationID { get; set; }

        public clsDetainedLicense()

        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.CreatedByUserID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.ReleaseDate = DateTime.MinValue;
            this.IsReleased = false;
            this.ReleasedByUserID = 0;
            this.ReleaseApplicationID = -1;

            Mode = enMode.AddNew;

        }

        public clsDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)

        {
            this.DetainID = DetainID;
            this.DetainID = DetainID;
            this.CreatedByUserID = CreatedByUserID;
            this.DetainID = DetainID;
            this.CreatedByUserInfo = clsUsers.Find(this.CreatedByUserID);
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.ReleaseDate = ReleaseDate;
            this.IsReleased = IsReleased;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleasedByUserInfo = clsUsers.Find(this.ReleasedByUserID);
            this.ReleaseApplicationID = ReleaseApplicationID;

            Mode = enMode.Update;
        }

        private bool _AddNewDetain()
        {
            //call DataAccess Layer 

            this.DetainID = clsDetainedLicenseData.AddNewDetain(this.LicenseID,this.DetainDate,this.CreatedByUserID,
              this.FineFees);

            return (this.DetainID != -1);
        }

        private bool _UpdateDetain()
        {
            //call DataAccess Layer 

            return clsDetainedLicenseData.UpdateDetain(this.DetainID,this.LicenseID, this.DetainDate, this.CreatedByUserID,
              this.FineFees);
        }

        public static clsDetainedLicense Find(int DetainID)
        {

            int LicenseID = -1; int CreatedByUserID = -1; DateTime DetainDate = DateTime.Now;
            float FineFees = 0; int ReleasedByUserID = -1; DateTime ReleaseDate = DateTime.MinValue;
            int ReleaseApplicationID = -1; bool IsReleased = false;

            if (clsDetainedLicenseData.GetDetainLicenseByDetainID(DetainID, ref LicenseID, ref CreatedByUserID, ref DetainDate,
               ref FineFees,ref IsReleased, ref ReleaseDate,ref ReleasedByUserID,ref ReleaseApplicationID))

                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees,
                 CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;

        }

        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {

            int DetainID = -1; int CreatedByUserID = -1; DateTime DetainDate = DateTime.Now;
            float FineFees = 0; int ReleasedByUserID = -1; DateTime ReleaseDate = DateTime.MinValue;
            int ReleaseApplicationID = -1; bool IsReleased = false;

            if (clsDetainedLicenseData.GetDetainLicenseByLicenseID(ref DetainID,LicenseID, ref CreatedByUserID, ref DetainDate,
               ref FineFees, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID,ref ReleaseApplicationID))

                return new clsDetainedLicense(DetainID,LicenseID,DetainDate,FineFees,
               CreatedByUserID, IsReleased, ReleaseDate,ReleasedByUserID,ReleaseApplicationID);
            else
                return null;

        }

        public static DataTable GetAllDetainLicense()
        {
            return clsDetainedLicenseData.GetAllDetainLicense();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetain())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDetain();

            }

            return false;
        }

        public static bool IsDetainLicense(int Liencense)
        {
            return clsDetainedLicenseData.IsDetainLicense(Liencense);
        }

        public  bool ReleaseDetainedLicense(int ReleaseUserID,int ReleaseApplicationID)
        {
            return clsDetainedLicenseData.ReleaseDetainLicense(this.DetainID, ReleaseUserID, ReleaseApplicationID);
        }

    }
}
