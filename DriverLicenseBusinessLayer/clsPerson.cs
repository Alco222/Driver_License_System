using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DriverLicenseDataAccessLayer;

namespace DriverLicenseBusinessLayer
{
    public class clsPerson
    {
      
        public enum enMode { AddNew = 1, Update = 2 };

        public enMode Mode = enMode.AddNew;

        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }
        public string NationalNo { get; set; }
        public byte Gender { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int NationalityID { get; set; }
        private string _ImagePath;

        public clsCountry countryInfo;
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.Gender = 0;
            this.NationalNo = "";
            this.Email = "";
            this.Phone = "";
            this.BirthDate = DateTime.Now;
            this.NationalityID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }

        private clsPerson(int personID, string firstName, string secondName, string thirdName,
            string lastName, string nationalNo, byte gender, string email, string address,
            string phone, DateTime birthDate, int nationalityID, string ImagePath)
        {
            this.PersonID = personID;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.NationalNo = nationalNo;
            this.Gender = gender;
            this.Email = email;
            this.Address = address;
            this.Phone = phone;
            this.BirthDate = birthDate;
            this.NationalityID = nationalityID;
            this.ImagePath = ImagePath;
            this.countryInfo = clsCountry.Find(nationalityID);
            Mode = enMode.Update;
        }


        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.FirstName, this.SecondName,
               this.ThirdName, this.LastName, this.NationalNo, this.Gender, this.Email,
               this.Address, this.Phone, this.BirthDate, this.NationalityID, this.ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.NationalNo, this.Gender, this.Email,
                this.Address, this.Phone, this.BirthDate, this.NationalityID,
                this.ImagePath);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;

                    }

                case enMode.Update:
                    return _UpdatePerson();


            }
            return false;

        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }
        public static clsPerson Find(int PersonID)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNo = "", Email = "", Address = "", ImagePath = "", Phone = "";
            byte Gender = 0;
            DateTime BirthDate = DateTime.Now;
            int NationalityID = -1;

            if (clsPersonData.GetPersonByID(PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref NationalNo, ref Gender, ref Email, ref Address, ref Phone, ref BirthDate, ref NationalityID, ref ImagePath))
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName, LastName, NationalNo, Gender, Email, Address, Phone, BirthDate, NationalityID, ImagePath);
            else
                return null;
        }

        public static clsPerson Find(string NationalNo)
        {
            int PersonID = 0;
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Address = "", ImagePath = "", Phone = "";
            byte Gender = 0;
            DateTime BirthDate = DateTime.Now;
            int NationalityID = -1;

            if (clsPersonData.GetPersonByNationalNo(ref PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, NationalNo, ref Gender, ref Email, ref Address, ref Phone, ref BirthDate, ref NationalityID, ref ImagePath))
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName, LastName, NationalNo, Gender, Email, Address, Phone, BirthDate, NationalityID, ImagePath);
            else
                return null;
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPerson();
        }

        public static bool IsPersonExists(int PersonID)
        {
            return clsPersonData.IsPesonExists(PersonID);
        }
        public static bool IsPersonExists(string NationalNo)
        {
            return clsPersonData.IsPesonExists(NationalNo);
        }

        public static int GetAgePerson(int PersonID)
        {
            int Age = 0;
            return clsPersonData.GetAgePerson(PersonID, ref Age);   
        }
        
    }
}
