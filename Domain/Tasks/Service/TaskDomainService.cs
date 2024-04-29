﻿using Domain.Tasks.Interfaces;
using Domain.Tasks.Models;
using Infra.Common.Result;

namespace Domain.Tasks.Service
{
    public class TaskDomainService : ITaskDomain
    {
        private readonly ITaskRepository _taskRepository;

        public TaskDomainService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Result<Models.Task>> AddAsync(Models.Task requestModel, CancellationToken cancellationToken)
        {
            requestModel.IdTask = Guid.NewGuid();
            requestModel.CreationDate = DateTime.Now;

            return await _taskRepository.AddAsync(requestModel, cancellationToken);
        }

        public async Task<Result<IEnumerable<TaskDTO>>> GetAllAsync(string userEmail, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetAllAsync(userEmail, cancellationToken);
        }

        public async Task<Result<TaskDTO>> GetByIdAsync(Guid idTask, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetByIdAsync(idTask, cancellationToken);
        }

        public async Task<Result<Models.Task>> UpdateAsync(Models.Task requestModel, CancellationToken cancellationToken)
        {
            return await _taskRepository.UpdateAsync(requestModel, cancellationToken);
        }
    }
}
