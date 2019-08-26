using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using HealthyWork.API.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthyWork.API.Services.Services
{
    public class HeadQuartersService: BaseService<HeadQuarters>, IService<HeadQuarters>
    {
        private readonly HealthyDbContext dbContext;

        public HeadQuartersService(HealthyDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultData<HeadQuarters>> Create(HeadQuarters model) => await CreateAsync(model);


        public async Task<ResultData<HeadQuarters>> Delete(Guid modelId) => await DeleteAsync(modelId);


        public async Task<ResultData<HeadQuarters>> Read(Guid modelId) => await ReadAsync(modelId);


        public async Task<ResultData<List<HeadQuarters>>> ReadAll() => await ReadAllAsync();

        public async Task<ResultData<HeadQuarters>> Update(HeadQuarters model) => await UpdateAsync(model, model.Id);

        public async Task<ResultData<List<HeadQuarters>>> ReadFiltered(HeadQuarters model, bool restricted) => await ReadFilteredAsync(model, restricted);
    }
}
