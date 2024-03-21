using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebCheckerAPI.DataModels;
using WebCheckerAPI.EntityFrameworkStuff;

namespace WebCheckerAPI.Filters
{
    public class NewRequest_ActionFilter : ActionFilterAttribute
    {
        private readonly RequestDbContext db;
        public NewRequest_ActionFilter(RequestDbContext db)
        {
            this.db = db;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            // or you could get Jason by "var flight = context.ActionArgument("flight") as a Flight"
            if (context.ActionArguments.TryGetValue("request", out var obj))
            {
                var request = obj as RequestModel;

                if (request == null)
                {
                    context.Result = new BadRequestObjectResult("You didn't send any value to be stored");
                    return;
                }


                var existingRequest = db.Request.FirstOrDefault(x => x.CardId == request.CardId);
                if (existingRequest != null)
                {
                    context.Result = new BadRequestObjectResult("The request with this Id already exists");
                    return;
                }


            }
        }
    }
}