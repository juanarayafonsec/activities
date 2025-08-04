using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Activities.Api.Extensions;
public static class ValidatorErrorExtension
{
    public static void AddCustomValidatorError(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = new Dictionary<string, string[]>();

                var allMissingProperties = new List<string>();

                foreach (var kvp in context.ModelState)
                {
                    if (kvp.Key == "$")
                    {
                        foreach (var err in kvp.Value.Errors)
                        {
                            var missingProps = ExtractMissingProperties(err.ErrorMessage);
                            if (missingProps != null)
                                allMissingProperties.AddRange(missingProps);
                        }
                    }
                    else if (kvp.Value.Errors.Count > 0)
                    {
                        errors[kvp.Key] = kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray();
                    }
                }

                if (allMissingProperties.Count > 0)
                {
                    var distinctProps = allMissingProperties.Distinct();
                    errors["missingProperties"] = new[] { $"Missing required properties: {string.Join(", ", distinctProps)}" };
                }

                var result = new
                {
                    type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    title = "One or more validation errors occurred.",
                    status = 400,
                    errors,
                    traceId = context.HttpContext.TraceIdentifier
                };

                return new BadRequestObjectResult(result);
            };
        });

        string[]? ExtractMissingProperties(string errorMessage)
        {
            // Match everything after the colon (:) up to the ending quote
            var match = Regex.Match(errorMessage, @":\s*(?<props>.+?)\.");

            if (match.Success)
            {
                var propsPart = match.Groups["props"].Value;

                var properties = propsPart.Split(',')
                                          .Select(p => p.Trim().Trim('\'', '"'))
                                          .ToArray();
                return properties;
            }

            return Array.Empty<string>();
        }
    }

}

