using Infra.Common.Errors;

namespace Domain.Tasks.Errors
{
    public static class TasksDomainErrors
    {
          
        public static Error TaskNaoPertenceAoEmail => new Error("Domain.Tasks.Errors.TaskNaoPertenceAoEmail", $"Esta Task não pertence ao usuário logado.");
        public static Error TaskNaoExiste => new Error("Domain.Tasks.Errors.TaskNaoExiste", $"Task não encontrada.");

    }
}
