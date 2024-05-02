using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GerenciadorCinema.Api.Filters;

public class NullResponseFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);

        if (context.Result is ObjectResult result)
        {
            if (result.Value == null)
            {
                context.Result = new NotFoundObjectResult(new { message = "Recurso não encontrado." });
            }
        }
    }
}
