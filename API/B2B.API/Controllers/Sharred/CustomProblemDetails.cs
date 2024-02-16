using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace B2B.API.Controllers.Sharred
{
    public class CustomProblemDetails : ProblemDetails
    {
        public List<string> Errors { get; private set; }

        public CustomProblemDetails(HttpStatusCode status, string? detail = null, IEnumerable<string>? errors = null) : this()
        {
            Title = status switch
            {
                HttpStatusCode.BadRequest => "Uma ou mais invalidaçoes ocorreram.",
                HttpStatusCode.InternalServerError => "Erro interno servidor.",
                _ => "Ocorreu um erro"
            };

            Status = (int)status;
            Detail = detail;

            if (errors is not null)
            {
                if (errors.Count() == 1)
                    Detail = errors.First();
                else if (errors.Count() > 1)
                    Detail = "Muiltiplos erros foram encontrados.";

                Errors.AddRange(errors);
            }
        }

        public CustomProblemDetails(HttpStatusCode status, HttpRequest request, string? detail = null, IEnumerable<string>? errors = null)
            : this(status, detail, errors) => Instance = request.Path;

        private CustomProblemDetails() =>
            Errors = new List<string>();
    }
}