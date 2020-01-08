using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json.Linq;

namespace Twenty.Devs
{
    public static class ModelStateExtention
    {
        private class ModelStateTransferValue
        {
            public string Key { get; set; }
            public string AttemptedValue { get; set; }
            public object RawValue { get; set; }
            public ICollection<string> ErrorMessages { get; set; } = new List<string>();
            public Type Type { get; set; }
        }

        
        public static void SaveToTempData(this ModelStateDictionary ModelSata, string KeyName, ITempDataDictionary TempData)
        {
            var errorList = ModelSata
                .Select(kvp => new ModelStateTransferValue
                {
                    Key             = kvp.Key,
                    AttemptedValue  = kvp.Value.AttemptedValue,
                    RawValue        = kvp.Value.RawValue,
                    ErrorMessages   = kvp.Value.Errors.Select(err => err.ErrorMessage).ToList(),
                    Type            = kvp.Value.RawValue?.GetType(),
                });

            TempData.Set(KeyName, errorList);
        }
        public static void RetrieveFromTempData(this ModelStateDictionary ModelState, string KeyName, ITempDataDictionary TempData)
        {
            var list = TempData.Get<List<ModelStateTransferValue>>(KeyName);

            if (list != null && list.Any())
                foreach (var item in list)
                {
                    var t = item.RawValue;

                    if (t.GetType() == typeof(JArray))
                    {
                        var values = ((JArray)t).ToObject(item.Type);
                        ModelState.SetModelValue(item.Key, values, item.AttemptedValue);
                    }
                    else
                        ModelState.SetModelValue(item.Key, item.RawValue, item.AttemptedValue);

                    foreach (var error in item.ErrorMessages)
                        ModelState.AddModelError(item.Key, error);
                }
        }
    }
}
