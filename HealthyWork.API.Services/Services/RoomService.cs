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
    public class RoomService: BaseService<Room>
    {
        private readonly HealthyDbContext dbContext;

        public RoomService(HealthyDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultData<Room>> CreateAsync(Room model) => await Create(model);


        public async Task<ResultData<Room>> DeleteAsync(Guid modelId) => await Delete(modelId);


        public async Task<ResultData<Room>> ReadAsync(Guid modelId) => await Read(modelId);


        public async Task<ResultData<List<Room>>> ReadAllAsync() => await ReadAll();

        public async Task<ResultData<Room>> UpdateAsync(Room model) => await Update(model, model.Id);
    }
}
