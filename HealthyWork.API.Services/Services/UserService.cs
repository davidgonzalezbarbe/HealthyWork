using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthyWork.API.Services.Services
{
    public class UserService: BaseService<User>
    {
        private readonly HealthyDbContext dbContext;

        public UserService(HealthyDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultData<User>> CreateAsync(User model) => await Create(model);


        public async Task<ResultData<User>> DeleteAsync(Guid modelId) => await Delete(modelId);


        public async Task<ResultData<User>> ReadAsync(Guid modelId) => await Read(modelId);


        public async Task<ResultData<List<User>>> ReadAllAsync() => await ReadAll();

        public async Task<ResultData<User>> UpdateAsync(User model) => await Update(model, model.Id);
    }
}
