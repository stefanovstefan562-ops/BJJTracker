using System.ComponentModel.DataAnnotations;

namespace BjjTracker.Validation
{
    public class VideoUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string url)
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    return ValidationResult.Success;
                }

                if (!Uri.TryCreate(url, UriKind.Absolute, out var uri)
                    || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                {
                    return new ValidationResult("The video link must be a valid http or https address.");
                }
            }

            return ValidationResult.Success;
        }
    }
}