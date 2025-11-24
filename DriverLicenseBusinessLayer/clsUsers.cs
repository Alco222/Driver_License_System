using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverLicenseDataAccessLayer;

namespace DriverLicenseBusinessLayer
{
    public class clsUsers
    {
        public enum enMode { AddNew = 1, Update = 2 };

        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;
        public clsUsers()
        {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;
            this.PersonID = -1;


            Mode = enMode.AddNew;
        }

        private clsUsers(int userID, string userName, string password, bool isActive, int personID)
        {
            this.UserID = userID;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;
            this.PersonID = personID;
            // Composition: clsUsers يحتوي على clsPerson
            this.PersonInfo = clsPerson.Find(personID);

            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsDataUsers.AddNewUser(this.UserName, clsSecurityHelper.ComputeHash( this.Password), this.IsActive, this.PersonID);

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsDataUsers.UpdateUser(this.UserID, this.UserName,clsSecurityHelper.ComputeHash(this.Password), this.IsActive, this.PersonID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;

                    }

                case enMode.Update:
                    return _UpdateUser();


            }
            return false;

        }

        public static bool DeleteUser(int UserID)
        {
            return clsDataUsers.DeleteUser(UserID);
        }

        public static clsUsers Find(int UserID)
        {
            string UserName = "", Password = "";
            bool IsActive = false;
            int PersonID = -1;

            if (clsDataUsers.GetUserByUserID(UserID, ref UserName, ref Password, ref IsActive, ref PersonID))
                return new clsUsers(UserID, UserName, Password, IsActive, PersonID);
            else
                return null;
        }

        public static clsUsers FindByUserName(string UserName)
        {
            int UserID = -1;
            string Password = "";
            bool IsActive = false;
            int PersonID = -1;

            if (clsDataUsers.GetUserByUserName(ref UserID, UserName, ref Password, ref IsActive, ref PersonID))
                return new clsUsers(UserID, UserName, Password, IsActive, PersonID);
            else
                return null;
        }

        public static clsUsers FindByPersonID(int PersonID)
        {
            string UserName = "", Password = "";
            bool IsActive = false;
            int UserID = -1;

            if (clsDataUsers.GetUserByPersonID(ref UserID, ref UserName, ref Password, ref IsActive, PersonID))
                return new clsUsers(UserID, UserName, Password, IsActive, PersonID);
            else
                return null;
        }

        public static clsUsers LoginUser(string UserName, string Password)
        {
            int UserID = -1;
            bool IsActive = false;
            int PersonID = -1;

            if ( clsDataUsers.LoginUser(ref UserID, UserName, clsSecurityHelper.ComputeHash(Password), ref IsActive, ref PersonID))
                return new clsUsers(UserID, UserName, Password, IsActive, PersonID);
            else
                return null;
        }

        public static async Task<clsUsers> LoginUserAsync(string UserName, string Password)
        {
            var result = await clsDataUsers.LoginUserAsync(UserName, clsSecurityHelper.ComputeHash(Password));

            if (result.IsFound)
                return new clsUsers(result.UserID, UserName, Password, result.IsActive, result.PersonID);
            else
                return null;
        }

        public static DataTable GetAllUser()
        {
            return clsDataUsers.GetAllUser();
        }

        public static bool IsUserExists(int UserID)
        {
            return clsDataUsers.IsUserExists(UserID);
        }

        public static bool IsUserExists(string UserName)
        {
            return clsDataUsers.IsUserExists(UserName);
        }

        public static bool IsUserExistsByPseronID(int PersonID)
        {
            return clsDataUsers.IsUserExistsByPersonID(PersonID);
        }

        public static bool ChangePassword(int UserID, string NewPassword)
        {
            return clsDataUsers.ChangePassword(UserID, clsSecurityHelper.ComputeHash(NewPassword));
        }
    }
}
