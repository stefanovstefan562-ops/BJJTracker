using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Validation
{
    public class DurationRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is TimeSpan duration)
            {
                if (duration < TimeSpan.FromMinutes(1) || duration > TimeSpan.FromHours(6))
                {
                    return new ValidationResult("The duration must be at least 1 minute and no more than 6 hours.");
                }
            }

            return ValidationResult.Success;
        }
    }
}