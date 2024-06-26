﻿using Infra.Common.Errors;

namespace WebApi.Errors.WebApi
{
    public static class TaskWebApiErrors
    {
        public static Error GenericError(string errorMessage)
        {
            return new("WebApi.Task.Errors.GenericError", $"Erro ocorreu na requisição, favor tentar novamente. ErrorMessage: {errorMessage}");
        }
    }
}
