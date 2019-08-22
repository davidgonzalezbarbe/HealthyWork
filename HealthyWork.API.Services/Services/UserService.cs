using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using HealthyWork.API.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthyWork.API.Services.Services
{
    public class UserService: BaseService<User>, IService<User>
    {
        private readonly HealthyDbContext dbContext;

        public UserService(HealthyDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultData<User>> Create(User model) => await CreateAsync(model);


        public async Task<ResultData<User>> Delete(Guid modelId) => await DeleteAsync(modelId);


        public async Task<ResultData<User>> Read(Guid modelId) => await ReadAsync(modelId);


        public async Task<ResultData<List<User>>> ReadAll() => await ReadAllAsync();

        public async Task<ResultData<User>> Update(User model) => await UpdateAsync(model, model.Id);
    }
}
