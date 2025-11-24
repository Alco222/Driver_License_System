using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverLicenseDataAccessLayer;
using static DriverLicenseBusinessLayer.clsApplication;

namespace DriverLicenseBusinessLayer
{
    /*  public class clsInternationalLicense : clsApplication
      {
          public enum enMode { AddNew = 0, Update = 1 };
          public enMode Mode = enMode.AddNew;

          public clsDriver DriverInfo;
          public int InternationalLicenseID { get; set; }
          public int IssuedUsedLocalLicenseID { get; set; }
          public int DriverID { get; set; }
          public DateTime IssueDate { get; set; }
          public DateTime ExpirationDate { get; set; }
          public bool IsActive { get; set; }



          public clsInternationalLicense()

          {
              //here we set the applicaiton type to New International License.
              this.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
              this.InternationalLicenseID = -1;
              this.DriverID = -1;
              this.IssuedUsedLocalLicenseID = -1;
              this.IssueDate = DateTime.Now;
              this.ExpirationDate = DateTime.Now;
              this.IsActive = true;


              Mode = enMode.AddNew;

          }

          public clsInternationalLicense(int ApplicationID, int ApplicantPersonID,
              DateTime ApplicationDate,enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
              float PaidFees, int CreatedByUserID,int InternationalLicenseID, int DriverID, int IssuedUsedLocalLicenseID,
              DateTime IssueDate, DateTime ExpirationDate,bool IsActive )
          {
              base.ApplicationID = ApplicationID;
              base.ApplicantPersonID = ApplicantPersonID;
              base.ApplicationDate = ApplicationDate;
              base.ApplicationStatus = ApplicationStatus;
              base.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
              base.LastStatusDate = LastStatusDate;
              base.PaidFees = PaidFees;
              base.CreatedByUserID = CreatedByUserID;

              this.InternationalLicenseID = InternationalLicenseID;
              this.ApplicationID = ApplicationID;
              this.DriverID = DriverID;
              this.IssuedUsedLocalLicenseID = IssuedUsedLocalLicenseID;
              this.IssueDate = IssueDate;
              this.ExpirationDate = ExpirationDate;
              this.IsActive = IsActive;
             this.CreatedByUserID = CreatedByUserID;

             this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);

              Mode = enMode.Update;
          }

          private bool _AddNewInternationalLicense()
          {
              //call DataAccess Layer 

              this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsedLocalLicenseID,
                 this.IssueDate, this.ExpirationDate, 
                 this.IsActive,this.CreatedByUserID);


              return (this.InternationalLicenseID != -1);
          }

          private bool _UpdateInternationalLicense()
          {
              //call DataAccess Layer 

              return clsInternationalLicenseData.UpdateInternationalLicense(this.IssuedUsedLocalLicenseID, this.ApplicationID,this.DriverID, this.IssuedUsedLocalLicenseID,
                 this.IssueDate, this.ExpirationDate,
                 this.IsActive, this.CreatedByUserID);
          }

          public static clsInternationalLicense Find(int InternationalLicenseID)
          {
              int ApplicationID = -1; int DriverID = -1; int IssuedUsedLocalLicenseID = -1;
              DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
              bool IsActive = true; int CreatedByUserID = 1;
              if (clsInternationalLicenseData.GetInternationalLicenseByID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsedLocalLicenseID,
              ref IssueDate, ref ExpirationDate,
              ref IsActive, ref CreatedByUserID))
              {

                  clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                  return new clsInternationalLicense(Application.ApplicationID,Application.ApplicantPersonID,
                  Application.ApplicationDate, (enApplicationStatus)Application.ApplicationStatus,Application.LastStatusDate,
                  Application.PaidFees,CreatedByUserID,  InternationalLicenseID,  DriverID,  IssuedUsedLocalLicenseID,
                  IssueDate, ExpirationDate,  IsActive);
              }
              else
                  return null;

          }

          public static DataTable GetAllInternationalLicense()
          {
              return clsInternationalLicenseData.GetAllInternationalLicense();

          }

          public bool Save()
          {
              //Because of inheritance first we call the save method in the base class,
              //it will take care of adding all information to the application table.
              base.Mode = (clsApplication.enMode)Mode;
              if (!base.Save())
                  return false;


              switch (Mode)
              {

                  case enMode.AddNew:
                      if (_AddNewInternationalLicense())
                      {

                          Mode = enMode.Update;
                          return true;
                      }
                      else
                      {
                          return false;
                      }

                  case enMode.Update:

                      return _UpdateInternationalLicense();

              }

              return false;
          }


          public static int GetActiveInternationalLicenseIDByDriverID( int DariverID)
          {
              return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DariverID);
          }

          public static DataTable GetDriverInternationalLicenses(int DriverID)
          {
              return clsInternationalLicenseData.GetDriverInternationaLicenses(DriverID);
          }

      }*/

    public class clsInternationalLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsDriver DriverInfo;
        public int InternationalLicenseID { get; set; }
        public int IssuedUsedLocalLicenseID { get; set; }
        public int DriverID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int ApplicationID {  get; set; }
        
        public clsApplication ApplicationInfo;

        public int CreatedByUserID;

        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsedLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;

            Mode = enMode.AddNew;
        }

        public clsInternationalLicense(int ApplicationID, int CreatedByUserID, int InternationalLicenseID, int DriverID, int IssuedUsedLocalLicenseID,
            DateTime IssueDate,DateTime ExpirationDate, bool IsActive)
        {
            
              /* int ApplicantPersonID,
               DateTime ApplicationDate, enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
               float PaidFees,*/
             

            /* base.ApplicationID = ApplicationID;
             base.ApplicantPersonID = ApplicantPersonID;
             base.ApplicationDate = ApplicationDate;
             base.ApplicationStatus = ApplicationStatus;
             base.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
             base.LastStatusDate = LastStatusDate;
             base.PaidFees = PaidFees;
             base.CreatedByUserID = CreatedByUserID;*/

            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsedLocalLicenseID = IssuedUsedLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);

            Mode = enMode.Update;
        }

        private bool _AddNewInternationalLicense()
        {
            //call DataAccess Layer 

            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsedLocalLicenseID,
               this.IssueDate, this.ExpirationDate,
               this.IsActive, this.CreatedByUserID);


            return (this.InternationalLicenseID != -1);
        }

        private bool _UpdateInternationalLicense()
        {
            //call DataAccess Layer 

            return clsInternationalLicenseData.UpdateInternationalLicense(this.IssuedUsedLocalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsedLocalLicenseID,
               this.IssueDate, this.ExpirationDate,
               this.IsActive, this.CreatedByUserID);
        }

        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID = -1; int DriverID = -1; int IssuedUsedLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true; int CreatedByUserID = 1;
            if (clsInternationalLicenseData.GetInternationalLicenseByID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsedLocalLicenseID,
            ref IssueDate, ref ExpirationDate,
            ref IsActive, ref CreatedByUserID))
            {

                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsInternationalLicense(Application.ApplicationID,  CreatedByUserID, InternationalLicenseID, DriverID, IssuedUsedLocalLicenseID,
                IssueDate, ExpirationDate, IsActive);
            }
            else
                return null;
           
            /*Application.ApplicantPersonID,
              Application.ApplicationDate, (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
              Application.PaidFees, */
            
        }

        public static DataTable GetAllInternationalLicense()
        {
            return clsInternationalLicenseData.GetAllInternationalLicense();

        }

        public bool Save()
        {
           /* //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;*/


            switch (Mode)
            {

                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateInternationalLicense();

            }

            return false;
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DariverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DariverID);
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverInternationaLicenses(DriverID);
        }

        public static bool DesactivateCurrentInternationalLicense(int DriverID)
        {
            int InternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(DriverID);
            
            return (clsInternationalLicenseData.DeactivateInternationalLicense(InternationalLicenseID));
        }

        public static clsInternationalLicense IssueInternationalLicense(int ApplicationPersonID,DateTime ApplicationDate,clsApplication.enApplicationStatus ApplicationStatus,float PaidFees,int CreatedByUserID,int DriverID,int IssuedUsedLocalLicenseID,DateTime IssueDate,DateTime ExpirationDate)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = ApplicationPersonID;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            Application.ApplicationDate = ApplicationDate;
            Application.ApplicationStatus = ApplicationStatus;
            Application.CreatedByUserID = CreatedByUserID;
            Application.PaidFees = PaidFees;
            
            if(!Application.Save())
            {
                return null;
            }

            //we need to deactivate the old International License.
            DesactivateCurrentInternationalLicense(DriverID);


            clsInternationalLicense NewInternationalLicense = new clsInternationalLicense();

            NewInternationalLicense.ApplicationID = Application.ApplicationID;
            NewInternationalLicense.DriverID = DriverID;
            NewInternationalLicense.IssuedUsedLocalLicenseID = IssuedUsedLocalLicenseID;
            NewInternationalLicense.IssueDate = IssueDate;
            NewInternationalLicense.ExpirationDate = ExpirationDate;
            NewInternationalLicense.CreatedByUserID = CreatedByUserID;

           if(!NewInternationalLicense.Save())
           {
                return null;
           }

          
           return NewInternationalLicense;

        }

    }

}
