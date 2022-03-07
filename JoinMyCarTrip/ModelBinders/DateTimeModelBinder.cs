using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace JoinMyCarTrip.ModelBinders
{
    public class DateTimeModelBinder : IModelBinder
    {
        private string customDateFormat;

        public DateTimeModelBinder(string _customDateFormat)
        {
            this.customDateFormat = _customDateFormat;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext
             .ValueProvider
             .GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueResult.FirstValue))
            {
                string dateValue = valueResult.FirstValue;

                var parsedDate = 
                    ParseCustomFormat(dateValue) ??
                    ParseBgFormat(dateValue) ??
                    ParseFallbackFormats(dateValue);

                if (parsedDate != null)
                {
                    bindingContext.Result = ModelBindingResult.Success(parsedDate);
                }
                else
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, $"Invalid date format: {dateValue}" );
                }

                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }

        private DateTime? ParseCustomFormat(string date)
        {
            if(DateTime.TryParseExact(date, customDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
            {
                return result;
            }

            return null;
        }

        private DateTime? ParseBgFormat(string date)
        {
            if (DateTime.TryParse(date, new CultureInfo("bg-BG"), DateTimeStyles.None, out var result))
            {
                return result;
            }

            return null;
        }

        private DateTime? ParseFallbackFormats(string date)
        {
            var CUSTOM_DATETIME_FORMATS = new string[]
            {
                "dd/MM/yyyy HH:mm",
                "dd.MM.yyyy HH:mm",
                "dd-MM-yyyy HH:mm",
            };

            if (DateTime.TryParseExact(
                    date, CUSTOM_DATETIME_FORMATS, CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime validDate))
            {
                return validDate;
            }

            return null;
        }
    }
}


