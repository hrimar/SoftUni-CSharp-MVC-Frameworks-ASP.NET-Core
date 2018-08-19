using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftUniClone.Web.Filters
{
public class ValidateModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var model = context.ActionArguments
                .FirstOrDefault(arg => arg.Key.ToLower().Contains("model")).Value;
            var controller = context.Controller as Controller;
            if (controller != null)
            {
                var view = controller.View(model);
                context.Result = view;
            }
        }
    }
}
}
