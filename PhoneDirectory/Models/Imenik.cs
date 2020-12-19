using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.Models
{
    public class PhoneDirectory
    {
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        String street;
        public String Street
        {
            get { return street; }
            set { street = value; }
        }

        String city;
        public String City
        {
            get { return city; }
            set { city = value; }
        }

        String country;
        public String Country
        {
            get { return country; }
            set { country = value; }
        }

        int phoneNumber;
        public int PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
    }
}
