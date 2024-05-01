using Infra.Common.Errors;

namespace Infra.Repository.Tasks.Errors
{
    public static class TasksRepositoryErrors
    {
        public static Error UnableToCreateTask(string errorMessage, string innerException)
        {
            return new("Repository.Tasks.Errors.UnableToCreateTask", $"Não foi possivel criar a Tarefa. Tente novamente. ErrorMessage: {errorMessage}. InnerException: {innerException}");
        }

        public static readonly Error UnableToRemove = new("Repository.Tasks.Errors.UnableToRemove", "Erro ao remover Task do banco de dados.");

        public static Error GenericErrorOnDelete(string errorMessage)
        {
            return new Error("Repository.Tasks.Errors.GenericErrorOnDelete", $"Erro ao remover Task do banco de dados. Message: {errorMessage}.");
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
