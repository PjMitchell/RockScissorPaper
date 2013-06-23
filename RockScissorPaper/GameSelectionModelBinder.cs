using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RockScissorPaper
{
    public class GameSelectionModelBinder : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueReceived = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var modelState = new ModelState() { Value = valueReceived };
            object result = null;

            if (valueReceived == null)
            {
                result = GameSelection.Ready;
            }
            else
            {
                int value = Convert.ToInt32(valueReceived.AttemptedValue);
                result = (GameSelection)value;
            }
            return result;
        }
    }
}