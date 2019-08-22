﻿using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthyWork.API.Services.Services
{
    public class HeadQuartersService: BaseService<HeadQuarters>
    {
        private readonly HealthyDbContext dbContext;

        public HeadQuartersService(HealthyDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultData<HeadQuarters>> CreateAsync(HeadQuarters model) => await Create(model);


        public async Task<ResultData<HeadQuarters>> DeleteAsync(Guid modelId) => await Delete(modelId);


        public async Task<ResultData<HeadQuarters>> ReadAsync(Guid modelId) => await Read(modelId);


        public async Task<ResultData<List<HeadQuarters>>> ReadAllAsync() => await ReadAll();

        public async Task<ResultData<HeadQuarters>> UpdateAsync(HeadQuarters model) => await Update(model, model.Id);
    }
}