using HoGi.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace HoGi.ToolsAndExtensions.Extensions
{
    public static class ModelStateExtension
    {
        public static AggregateException<HoGiException> ToHoGiException(this ModelStateDictionary states)
        {
            var aggregateException = new AggregateException<HoGiException>();
            foreach (var item in states.Where(x => x.Value.ValidationState == ModelValidationState.Invalid))
            {
                aggregateException.Errors.Add(new GeneralException($"{item.Value.Errors.FirstOrDefault()?.ErrorMessage ?? ""}"));
            }

            return aggregateException;
        }
    }
}
