using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using HealthyWork.API.Contracts.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HealthyWork.API.Services.Services
{
    public class ValueService: BaseService<Value>, IService<Value>
    {
        private readonly HealthyDbContext dbContext;

        public ValueService(HealthyDbContext dbContext): base (dbContext)
        {
            this.dbContext = dbContext;
        }

       public async Task<ResultData<Value>> Create(Value model) => await CreateAsync(model);


        public async Task<ResultData<Value>> Delete(Guid modelId) => await DeleteAsync(modelId);


        public async Task<ResultData<Value>> Read(Guid modelId) => await ReadAsync(modelId);


        public async Task<ResultData<List<Value>>> ReadAll() => await ReadAllAsync();

        public async Task<ResultData<Value>> Update(Value model) => await UpdateAsync(model, model.Id);

        public async Task<ResultData<List<Value>>> ReadFiltered(Value model, bool restricted) => await ReadFilteredAsync(model, restricted);
    }
}
