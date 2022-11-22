using System.ComponentModel.DataAnnotations;

namespace Logistic.DAL.Attributes.Validation
{
    public class NotEqualAttribute : ValidationAttribute
    {
        private string OtherProperty { get; set; }
        private string MainPropertyName { get; }
        private string OtherPropertyName { get; }

        public NotEqualAttribute(string otherProperty, string mainPropertyName, string otherPropertyName)
        {
            OtherProperty = otherProperty;
            MainPropertyName = mainPropertyName;
            OtherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            var otherValue = otherPropertyInfo?.GetValue(validationContext.ObjectInstance);

            return value?.ToString()?.Equals(otherValue?.ToString()) is true or null
                ? new ValidationResult($"{MainPropertyName} не должно быть равно {OtherPropertyName}.")
                : ValidationResult.Success;
        }
    }
}