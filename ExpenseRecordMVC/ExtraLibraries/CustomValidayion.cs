using System.ComponentModel.DataAnnotations;

namespace ExtraLibraries
{
    public class CustomValidayion : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTimeOffset date = DateTimeOffset.Parse(value.ToString());
                if (date <= DateTimeOffset.Now)
                {
                    return ValidationResult.Success;
                }
            }
            
            return new ValidationResult("Invalid Date.");
            
        }
    }
}
