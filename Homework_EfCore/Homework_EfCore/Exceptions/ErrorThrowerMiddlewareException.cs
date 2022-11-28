namespace Homework_EfCore.Exceptions
{
    public class ErrorThrowerMiddlewareException
    {
        private readonly RequestDelegate _next;

        public ErrorThrowerMiddlewareException(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                await httpContext.Response.WriteAsync(ex.Message);
            }
            catch (ObjectNotFoundException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                await httpContext.Response.WriteAsync(ex.Message);
            }
            catch (IncorrectValueException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status418ImATeapot;
                await httpContext.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsync(ex.Message);
            }

        }
    }
}

