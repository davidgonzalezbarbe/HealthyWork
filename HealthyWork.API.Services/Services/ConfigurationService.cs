using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthyWork.API.Services.Services
{
    public class ConfigurationService : BaseService<Configuration>
    {
        private readonly HealthyDbContext dbContext;

        public ConfigurationService(HealthyDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultData<Configuration>> CreateAsync(Configuration model) => await Create(model);


        public async Task<ResultData<Configuration>> DeleteAsync(Guid modelId) => await Delete(modelId);


        public async Task<ResultData<Configuration>> ReadAsync(Guid modelId) => await Read(modelId);


        public async Task<ResultData<List<Configuration>>> ReadAllAsync() => await ReadAll();

        public async Task<ResultData<Configuration>> UpdateAsync(Configuration model) => await Update(model, model.Id);
    }
}
