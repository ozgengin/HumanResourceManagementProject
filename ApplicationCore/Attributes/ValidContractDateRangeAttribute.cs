using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Attributes
{
    public class ValidContractDateRangeAttribute : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public ValidContractDateRangeAttribute(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var startDateProperty = validationContext.ObjectType.GetProperty("ContractStartDate");
            if (startDateProperty == null)
            {
                throw new ArgumentException("Invalid property name");
            }

            var startDate = (DateTime)startDateProperty.GetValue(validationContext.ObjectInstance, null);

            if ((DateTime)value < startDate)
            {
                return new ValidationResult(ErrorMessage ?? "Contract end date cannot be earlier than contract start date");
            }

            return ValidationResult.Success;
        }
    }
}
