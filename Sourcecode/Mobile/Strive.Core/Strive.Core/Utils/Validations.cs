using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Strive.Core.Utils
{
    public static class Validations
    {
        public static bool validateEmail(string email)
        {
            if (email == "" ||email == null) return false;

            Regex regex = new Regex(@"(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*|" + "\"" + @"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*" + "\"" + @")@(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-zA-Z0-9-]*[a-zA-Z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])");
            Match match = regex.Match(email);
            if (match.Success)
            {
                return true;
            }

            return true;
        }
        public static bool validateEmailEmployeeID(string email)
        {
            if (email == "" || email == null) return false;

            //Regex regex = new Regex(@"(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*|" + "\"" + @"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*" + "\"" + @")@(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-zA-Z0-9-]*[a-zA-Z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])");
            //Match match = regex.Match(email);
            //if (match.Success)
            //{
            //    return true;
            //}

            return true;
        }

        public static bool validatePhone(string phone)
        {
            //valid US country numbers with country-code accepted 

            if (phone == null) return false;

            Regex phoneRegex = new Regex(@"^(\d{1,2}\s?)?(\(?\d{3}\)?)[\s.-]?\d{3}[\s.-]?\d{4}$");
            Match phoneMatch = phoneRegex.Match(phone);
            if (phoneMatch.Success)
            {
                return true;
            }

            return false;
        }
    }
}
