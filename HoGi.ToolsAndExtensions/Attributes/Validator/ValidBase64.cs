using System;
using System.ComponentModel.DataAnnotations;

namespace HoGi.Commons.ToolsAndExtensions.Attributes.Validator
{
    public class ValidBase64 : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {

                var base64Data = (string)value;

                var _ = Convert.FromBase64String(base64Data);

                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}