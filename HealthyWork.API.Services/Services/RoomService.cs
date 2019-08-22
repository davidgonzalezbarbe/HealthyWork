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
    public class RoomService: BaseService<Room>, IService<Room>
    {
        private readonly HealthyDbContext dbContext;

        public RoomService(HealthyDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultData<Room>> Create(Room model) => await CreateAsync(model);


        public async Task<ResultData<Room>> Delete(Guid modelId) => await DeleteAsync(modelId);


        public async Task<ResultData<Room>> Read(Guid modelId) => await ReadAsync(modelId);


        public async Task<ResultData<List<Room>>> ReadAll() => await ReadAllAsync();

        public async Task<ResultData<Room>> Update(Room model) => await UpdateAsync(model, model.Id);
    }
}
