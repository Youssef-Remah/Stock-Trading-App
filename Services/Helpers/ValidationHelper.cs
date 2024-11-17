using System.ComponentModel.DataAnnotations;

namespace Services.Helpers
{
    internal class ValidationHelper
    {
        internal static void ModelValidator(object obj)
        {
            ValidationContext validationContext = new(obj);

            List<ValidationResult> validationResults = new();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}