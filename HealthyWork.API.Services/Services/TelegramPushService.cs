using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthyWork.API.Services.Services
{
    public class TelegramPushService : BaseService<TelegramPush>
    {
        private readonly HealthyDbContext dbContext;

        public TelegramPushService(HealthyDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultData<TelegramPush>> CreateAsync(TelegramPush model) => await Create(model);


        public async Task<ResultData<TelegramPush>> DeleteAsync(Guid modelId) => await Delete(modelId);


        public async Task<ResultData<TelegramPush>> ReadAsync(Guid modelId) => await Read(modelId);


        public async Task<ResultData<List<TelegramPush>>> ReadAllAsync() => await ReadAll();

        public async Task<ResultData<TelegramPush>> UpdateAsync(TelegramPush model) => await Update(model, model.Id);
    }
}
