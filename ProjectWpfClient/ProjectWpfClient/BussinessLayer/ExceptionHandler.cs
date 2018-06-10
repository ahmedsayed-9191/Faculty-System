using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectWpfClient
{
    class ExceptionHandler
    {
        // Function to Check String
        public Boolean IsValidString(string str)
        {
            Regex regex = new Regex(@"^[a-zA-Z]+$");

            if (regex.IsMatch(str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Function to Check Email Correctness
        public Boolean IsValidEmail(string email)
        {
            string pattern = null;
            pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (Regex.IsMatch(email, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Function to Validate User Name
        public bool IsValidUserName(string username)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9_]+$");

            if (regex.IsMatch(username))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        //Function to Validate Description
        public bool IsValidDescription(string username)
        {
            Regex regex = new Regex(@"^[a-zA-Z ,\n]+$");

            if (regex.IsMatch(username))
            {
                return true;
            }
            else
            {
                return false;
            }


        }



    }
}
