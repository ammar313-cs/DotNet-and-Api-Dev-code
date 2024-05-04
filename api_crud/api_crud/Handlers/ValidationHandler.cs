using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using api_crud.Validation_Configuration;
using Microsoft.Extensions.Configuration;

namespace api_crud.validation_handler
{
    public class DynamicValidationService
    {
        private readonly IConfiguration _configuration;

        public DynamicValidationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ApplyValidations(object model)
        {
            // Get the name of the model type
            var modelName = model.GetType().Name;

            // Get the validation configuration from the appsettings using the model name
            var validationConfig = _configuration.GetSection($"ValidationConfig:{modelName}")
                                                  .Get<ValidationConfig[]>();

            // Create a validation context for the model
            var validationContext = new ValidationContext(model);

            // Create a list to store validation results
            var validationResults = new List<ValidationResult>();

            foreach (var config in validationConfig)
            {
                // Get the value of the property using reflection
                var value = model.GetType().GetProperty(config.PropertyName)?.GetValue(model);

                // Set the member name for the validation context
                validationContext.MemberName = config.PropertyName;

                // Validate required property
                if (config.Required.HasValue && config.Required.Value)
                {
                    Validator.ValidateProperty(value, validationContext);
                }

                // Validate max length
                if (config.MaxLength.HasValue && value is string stringValue)
                {
                    if (stringValue.Length > config.MaxLength.Value)
                    {
                        validationResults.Add(new ValidationResult($"{config.PropertyName} exceeds max length of {config.MaxLength.Value}"));
                    }
                }

                // Validate regex pattern
                if (!string.IsNullOrEmpty(config.RegexPattern) && value is string regexValue)
                {
                    var regex = new Regex(config.RegexPattern);
                    if (!regex.IsMatch(regexValue))
                    {
                        validationResults.Add(new ValidationResult(config.RegexErrorMessage));
                    }
                }

                // Validate email address
                if (config.EmailAddress.HasValue && config.EmailAddress.Value && value is string emailValue)
                {
                    var emailAddressAttribute = new EmailAddressAttribute();
                    if (!emailAddressAttribute.IsValid(emailValue))
                    {
                        validationResults.Add(new ValidationResult("Invalid email address."));
                    }
                }
            }

            // If there are validation errors, throw a ValidationException
            if (validationResults.Any())
            {
                var errorMessages = string.Join(Environment.NewLine, validationResults.Select(result => result.ErrorMessage));
                throw new ValidationException(errorMessages);
            }
        }
    }
}
