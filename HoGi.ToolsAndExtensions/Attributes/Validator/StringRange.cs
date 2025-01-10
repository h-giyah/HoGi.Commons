using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HoGi.ToolsAndExtensions.Attributes.Validator
{
    public class StringRangeAttribute : ValidationAttribute
    {
        public string[] AllowableValues { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (AllowableValues?.Contains(value?.ToString()) == true) return ValidationResult.Success;

            var message =
                $"Please enter one of the allowable values: {string.Join(", ", AllowableValues ?? new[] { "No allowable values found" })}.";
            return new ValidationResult(message);
        }
    } //end class
}