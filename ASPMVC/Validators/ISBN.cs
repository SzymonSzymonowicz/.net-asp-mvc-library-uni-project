using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ASPMVC.Validators
{
    public class ISBN : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string isbn = (string) value;

            if (isbn == null)
                return false;

            //remove any hyphens
            isbn = isbn.Replace("-", "");

            //must be a 13 digit ISBN
            if (isbn.Length != 13)
                return false;

            try
            {
                int total = 0;
                for (int i = 0; i < 12; i++)
                {
                    int digit = Int16.Parse(isbn.Substring(i, 1));
                    total += (i % 2 == 0) ? digit * 1 : digit * 3;
                }

                //checksum must be 0-9. If calculated as 10 then = 0
                int checksum = 10 - (total % 10);
                if (checksum == 10)
                {
                    checksum = 0;
                }

                return checksum == Int16.Parse(isbn.Substring(12));
            }
            catch (FormatException fe)
            {
                //to catch invalid ISBNs with non-numeric characters in them
                return false;
            }
        }
    }
}
