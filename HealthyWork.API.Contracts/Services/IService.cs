using HealthyWork.API.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthyWork.API.Contracts.Services
{
    public interface IService<T> where T : class
    {
        Task<ResultData<T>> Create(T model);

        Task<ResultData<T>> Read(Guid modelId);

        Task<ResultData<List<T>>> ReadAll();

        Task<ResultData<T>> Update(T model);

        Task<ResultData<T>> Delete(Guid modelId);

    }

}
