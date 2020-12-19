using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.Models
{
    public class ImenikRegistracija
    {
        List<PhoneDirectory> directoryList;
        static ImenikRegistracija direcregd = null;
        private ImenikRegistracija()
        {
            directoryList = new List<PhoneDirectory>();
        }
        public static ImenikRegistracija getInstance()
        {
            if (direcregd == null)
            {
                direcregd = new ImenikRegistracija();
                return direcregd;
            }
            else
            {
                return direcregd;
            }
        }
        public void Add(PhoneDirectory directory)
        {
            directoryList.Add(directory);
        }
        public String Remove(String id)
        {
            for (int i = 0; i < directoryList.Count; i++)
            {
                PhoneDirectory dir = directoryList.ElementAt(i);
                if (dir.ID.Equals(id))
                {
                    directoryList.RemoveAt(i);//update the new record
                    return "Delete successful";
                }
            }
            return "Delete un-successful";
        }
        public List<PhoneDirectory> getAllStudent()
        {
            return directoryList;
        }
        public String UpdateStudent(PhoneDirectory std)
        {
            for (int i = 0; i < directoryList.Count; i++)
            {
                PhoneDirectory dir = directoryList.ElementAt(i);
                if (dir.ID.Equals(std.ID))
                {
                    directoryList[i] = std;//update the new record
                    return "Update successful";
                }
            }
            return "Update un-successful";
        }
    }
}

