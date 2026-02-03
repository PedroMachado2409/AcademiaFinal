namespace NexusGym.Infrastructure.ProblemDetails
{
    public static class ProblemDetailsFactory
    {
        public static object Unauthorized(string detail)
            => new
            {
                title = "Não autorizado",
                status = StatusCodes.Status401Unauthorized,
                detail
            };

        public static object InternalServerError(string detail)
            => new
            {
                title = "Erro interno no servidor",
                status = StatusCodes.Status500InternalServerError,
                detail
            }; 
        
        public static object BadRequest(string detail)
            => new
            {
                title = "Ocorreu um erro ao enviar a requisição",
                status = StatusCodes.Status400BadRequest,
                detail
            };
    }

}
