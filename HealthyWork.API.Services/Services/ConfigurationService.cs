using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using HealthyWork.API.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthyWork.API.Services.Services
{
    public class ConfigurationService : BaseService<Configuration>, IService<Configuration>
    {
        private readonly HealthyDbContext dbContext;

        public ConfigurationService(HealthyDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultData<Configuration>> Create(Configuration model) => await CreateAsync(model);


        public async Task<ResultData<Configuration>> Delete(Guid modelId) => await DeleteAsync(modelId);


        public async Task<ResultData<Configuration>> Read(Guid modelId) => await ReadAsync(modelId);


        public async Task<ResultData<List<Configuration>>> ReadAll() => await ReadAllAsync();

        public async Task<ResultData<Configuration>> Update(Configuration model) => await UpdateAsync(model, model.Id);
    }
}
