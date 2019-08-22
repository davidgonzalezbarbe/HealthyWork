using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using HealthyWork.API.Contracts.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace HealthyWork.API.Services.Services
{
    public class BaseService<T> where T : class
    {
        private readonly HealthyDbContext dbContext;

        public BaseService(HealthyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected async Task<ResultData<T>> CreateAsync(T model)
        {
            ResultData<T> result = new ResultData<T>() { Content = null };

            try
            {
                var res = dbContext.Set<T>().Add(model);
                await dbContext.SaveChangesAsync();
                result.AddResult(GetResponseCode(nameof(T) + "_Created").GetCode(), GetResponseCode(nameof(T) + "_Created").GetDescription(), ResultType.Success);
                result.Content = res.Entity;
            }
            catch (Exception)
            {
                result.AddResult(ResponseCode.Exception_Create.GetCode(), ResponseCode.Exception_Create.GetDescription(MethodBase.GetCurrentMethod().DeclaringType.Name), ResultType.Exception);
            }
            return result;
        }

        protected async Task<ResultData<T>> DeleteAsync(Guid modelId)
        {
            ResultData<T> result = new ResultData<T>() { Content = null };

            try
            {
                var model = await dbContext.Set<T>().FindAsync(modelId);
                if (model == null) throw new NullReferenceException();
                var res = dbContext.Set<T>().Remove(model);
                await dbContext.SaveChangesAsync();

                result.AddResult(GetResponseCode(nameof(T) + "_Deleted").GetCode(), GetResponseCode(nameof(T) + "_Deleted").GetDescription(), ResultType.Success);
                result.Content = res.Entity;
            }
            catch (NullReferenceException)
            {
                result.AddResult(GetResponseCode(nameof(T) + "_NotDeleted").GetCode(), GetResponseCode(nameof(T) + "_NotDeleted").GetDescription(), ResultType.Error);
            }
            catch (Exception)
            {
                result.AddResult(ResponseCode.Exception_Delete.GetCode(), ResponseCode.Exception_Delete.GetDescription(MethodBase.GetCurrentMethod().DeclaringType.Name), ResultType.Exception);
            }
            return result;
        }

        protected async Task<ResultData<T>> ReadAsync(Guid modelId)
        {
            ResultData<T> result = new ResultData<T>() { Content = null };

            try
            {
                var model = await dbContext.Set<T>().FindAsync(modelId);
                result.Content = model ?? throw new NullReferenceException();
                result.AddResult(GetResponseCode(nameof(T) + "_Found").GetCode(), GetResponseCode(nameof(T) + "_Found").GetDescription(), ResultType.Success);
            }
            catch (NullReferenceException)
            {
                result.AddResult(GetResponseCode(nameof(T) + "_NotFound").GetCode(), GetResponseCode(nameof(T) + "_NotFound").GetDescription(), ResultType.Error);
            }
            catch (Exception)
            {
                result.AddResult(ResponseCode.Exception_Read.GetCode(), ResponseCode.Exception_Read.GetDescription(MethodBase.GetCurrentMethod().DeclaringType.Name), ResultType.Exception);
            }
            return result;
        }

        protected async Task<ResultData<List<T>>> ReadAllAsync()
        {
            ResultData<List<T>> result = new ResultData<List<T>>() { Content = null };

            try
            {
                var model = await dbContext.Set<T>().ToListAsync();
                result.Content = model ?? throw new NullReferenceException();
                result.AddResult(GetResponseCode(nameof(T) + "s_Found").GetCode(), GetResponseCode(nameof(T) + "s_Found").GetDescription(), ResultType.Success);
            }
            catch (NullReferenceException)
            {
                result.AddResult(GetResponseCode(nameof(T) + "s_NotFound").GetCode(), GetResponseCode(nameof(T) + "s_NotFound").GetDescription(), ResultType.Error);
            }
            catch (Exception)
            {
                result.AddResult(ResponseCode.Exception_Read.GetCode(), ResponseCode.Exception_Read.GetDescription(MethodBase.GetCurrentMethod().DeclaringType.Name), ResultType.Exception);
            }
            return result;
        }
        protected async Task<ResultData<T>> UpdateAsync(T model, Guid id)
        {
            ResultData<T> result = new ResultData<T>() { Content = null };

            try
            {
                var found = await dbContext.Set<T>().FindAsync(id);
                if (found == null) throw new NullReferenceException();
                dbContext.Entry(found).State = EntityState.Detached;
                var res = dbContext.Set<T>().Update(model);
                result.AddResult(GetResponseCode(nameof(T) + "_Updated").GetCode(), GetResponseCode(nameof(T) + "_Updated").GetDescription(), ResultType.Success);
                result.Content = res.Entity;
            }
            catch (NullReferenceException)
            {
                result.AddResult(GetResponseCode(nameof(T) + "_NotUpdated").GetCode(), GetResponseCode(nameof(T) + "_NotUpdated").GetDescription(), ResultType.Error);
            }
            catch (Exception)
            {
                result.AddResult(ResponseCode.Exception_Update.GetCode(), ResponseCode.Exception_Update.GetDescription(MethodBase.GetCurrentMethod().DeclaringType.Name), ResultType.Exception);
            }
            return result;
        }

        private ResponseCode GetResponseCode(string value)
        {
            return (ResponseCode)(Enum.Parse(typeof(ResponseCode), value));
        }
    }

}
