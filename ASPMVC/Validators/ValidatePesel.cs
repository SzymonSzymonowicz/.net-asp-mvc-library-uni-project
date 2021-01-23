using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ASPMVC.Validators
{
    public class ValidatePesel : ValidationAttribute
    {
        public string errorLenght = "PESEL must consist of 11 digits.";
        public string errorPesel = "PESEL number not valid!";
        public string errorType = "PESEL must consist only of digits.";
        public string errorDate = "Date of birth not matching PESEL!";

        private readonly string _dateOfBirth;
        public ValidatePesel(string dobPropName)
        {
            _dateOfBirth = dobPropName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string pesel = value.ToString();

            if (!decimal.TryParse(pesel, out decimal result_status))
                return new ValidationResult(errorType);

            if (pesel.Length != 11)
                return new ValidationResult(errorLenght);

            var dobProp = validationContext.ObjectType.GetProperty(_dateOfBirth);
            var dob = (DateTime) dobProp.GetValue(validationContext.ObjectInstance, null);

            string peselDob = pesel.Substring(0, 6);

            int peselYear = int.Parse(peselDob.Substring(0, 2));
            int peselMonth = int.Parse(peselDob.Substring(2, 2));
            int peselDay= int.Parse(peselDob.Substring(4, 2));

            int dobYear = dob.Year;
            int dobMonth = dob.Month;
            int dobDay = dob.Day;

            // for people born in 2000-2099
            if (dobYear >= 2000)
                dobMonth += 20;

            if ((dobYear % 100) != peselYear || dobMonth != peselMonth || dobDay != peselDay)
            {
                return new ValidationResult(errorDate);
            }


            // checking control sum
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int sum = 0;
            // pesel last digit
            int controlNum = int.Parse(pesel.Substring(10, 1));

            for (int i = 0; i < weights.Length; i++)
            {
                sum += int.Parse(pesel.Substring(i, 1)) * weights[i];
            }

            sum = sum % 10;

            if (10 - sum != controlNum)
                return new ValidationResult(errorPesel);

            return ValidationResult.Success;
        }
    }
}
