﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconITScrapperWebAPI.ActionFilters
{
    public class ValidationFilterModelAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Microsoft.AspNetCore.Mvc.Controller controller = context.Controller as Controller;
            if (controller != null)
            {
                var model = controller.ViewData.Model;
                if (model == null)
                {
                    context.Result = new BadRequestObjectResult("Could not find the object");
                    return;
                }
                else
                {
                    context.Result = new OkObjectResult(model);
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                //string messages = string.Join("; ", context.ModelState.Values
                //                        .SelectMany(x => x.Errors)
                //                        .Select(x => x.ErrorMessage));
                //context.Result = new BadRequestObjectResult("");
            }
        }
    }
}
