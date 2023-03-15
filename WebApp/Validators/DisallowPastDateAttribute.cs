using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System;

namespace WebApp.Validators
{
	public class DisallowPastDateAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var dt = (DateTime)value;
			if (dt >= DateTime.Now)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("Date cannot be in the past."); ;
		}
	}
}
