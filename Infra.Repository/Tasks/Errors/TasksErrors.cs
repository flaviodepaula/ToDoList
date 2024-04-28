using Infra.Common.Errors;

namespace Infra.Repository.Tasks.Errors
{
    public static class TasksErrors
    {
        public static Error UnableToCreateTask(string errorMessage, string innerException)
        {
            return new("Repository.Tasks.Errors.UnableToCreateTask", $"Não foi possivel criar a Tarefa. Tente novamente. ErrorMessage: {errorMessage}. InnerException: {innerException}");
        }

        public static Error RequestToDatabaseFailed(string errorMessage)
        {
            return new Error("Repository.Tasks.Errors.RequestToDatabaseFailed", $"Requisição para o banco de dados falhou. Message: {errorMessage}.");
        }
        public static Error DeleteGeneralException(string errorMessage)
        {
            return new Error("Repository.Tasks.Errors.DeleteGeneralException", $"Erro ao deletar registro. Message: {errorMessage}.");
        }

        public static Error InfoDoesNotExist => new Error("Repository.Tasks.Errors.InfoDoesNotExist", $"Registro não encontrado na base de dados.");

    }
}
