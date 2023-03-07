using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System;

namespace WebApp.Validators
{
	public class BeforeEndDateAttribute : ValidationAttribute
	{
		public string EndDatePropertyName { get; set; }
		public string StartDatePropertyName { get; set; }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			PropertyInfo endDateProperty = validationContext.ObjectType.GetProperty(EndDatePropertyName);
			DateTime endDate = (DateTime)endDateProperty.GetValue(validationContext.ObjectInstance, null);
			PropertyInfo startDateProperty = validationContext.ObjectType.GetProperty(StartDatePropertyName);
			DateTime startDate = (DateTime)startDateProperty.GetValue(validationContext.ObjectInstance, null);
			if (startDate >= endDate)
			{
				return new ValidationResult($"{StartDatePropertyName} must precede {EndDatePropertyName}.");
			}
			return ValidationResult.Success;
		}
	}
}
