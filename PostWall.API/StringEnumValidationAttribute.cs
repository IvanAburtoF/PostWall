using System.ComponentModel.DataAnnotations;

public class StringEnumValidationAttribute : ValidationAttribute
{
    private readonly Type _enumType;

    public StringEnumValidationAttribute(Type enumType)
    {
        _enumType = enumType;
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("Type must be an enumeration", nameof(enumType));
        }
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || !_enumType.GetEnumNames().Contains(value.ToString(), StringComparer.OrdinalIgnoreCase))
        {
            return new ValidationResult($"The value '{value}' is not valid for enum type {_enumType.Name}.");
        }

        return ValidationResult.Success;
    }
}
