using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using HealthyWork.API.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthyWork.API.Services.Services
{
    public class TelegramPushService : BaseService<TelegramPush>, IService<TelegramPush>
    {
        private readonly HealthyDbContext dbContext;

        public TelegramPushService(HealthyDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultData<TelegramPush>> Create(TelegramPush model) => await CreateAsync(model);


        public async Task<ResultData<TelegramPush>> Delete(Guid modelId) => await DeleteAsync(modelId);


        public async Task<ResultData<TelegramPush>> Read(Guid modelId) => await ReadAsync(modelId);


        public async Task<ResultData<List<TelegramPush>>> ReadAll() => await ReadAllAsync();

        public async Task<ResultData<TelegramPush>> Update(TelegramPush model) => await UpdateAsync(model, model.Id);
    }
}
