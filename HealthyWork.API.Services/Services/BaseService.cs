using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using HealthyWork.API.Contracts.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                typeof(T).GetProperty("Id").SetValue(model, Guid.NewGuid());
                var res = dbContext.Set<T>().Add(model);
                await dbContext.SaveChangesAsync();
                result.AddResult(GetResponseCode(typeof(T).Name + "_Created").GetCode(), GetResponseCode(typeof(T).Name + "_Created").GetDescription(), ResultType.Success);
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

                result.AddResult(GetResponseCode(typeof(T).Name + "_Deleted").GetCode(), GetResponseCode(typeof(T).Name + "_Deleted").GetDescription(), ResultType.Success);
                result.Content = res.Entity;
            }
            catch (NullReferenceException)
            {
                result.AddResult(GetResponseCode(typeof(T).Name + "_NotDeleted").GetCode(), GetResponseCode(typeof(T).Name + "_NotDeleted").GetDescription(), ResultType.Error);
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
                result.AddResult(GetResponseCode(typeof(T).Name + "_Found").GetCode(), GetResponseCode(typeof(T).Name + "_Found").GetDescription(), ResultType.Success);
            }
            catch (NullReferenceException)
            {
                result.AddResult(GetResponseCode(typeof(T).Name + "_NotFound").GetCode(), GetResponseCode(typeof(T).Name + "_NotFound").GetDescription(), ResultType.Error);
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
                result.AddResult(GetResponseCode(typeof(T).Name + "s_Found").GetCode(), GetResponseCode(typeof(T).Name + "s_Found").GetDescription(), ResultType.Success);
            }
            catch (NullReferenceException)
            {
                result.AddResult(GetResponseCode(typeof(T).Name + "s_NotFound").GetCode(), GetResponseCode(typeof(T).Name + "s_NotFound").GetDescription(), ResultType.Error);
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
                result.AddResult(GetResponseCode(typeof(T).Name + "_Updated").GetCode(), GetResponseCode(typeof(T).Name + "_Updated").GetDescription(), ResultType.Success);
                result.Content = res.Entity;
            }
            catch (NullReferenceException)
            {
                result.AddResult(GetResponseCode(typeof(T).Name + "_NotUpdated").GetCode(), GetResponseCode(typeof(T).Name + "_NotUpdated").GetDescription(), ResultType.Error);
            }
            catch (Exception)
            {
                result.AddResult(ResponseCode.Exception_Update.GetCode(), ResponseCode.Exception_Update.GetDescription(MethodBase.GetCurrentMethod().DeclaringType.Name), ResultType.Exception);
            }
            return result;
        }

        public async Task<ResultData<List<T>>> ReadFiltered(T model, bool restricted)
        {
            ResultData<List<T>> result = new ResultData<List<T>>() { Content = null };

            try
            {
                List<PropertyInfo> notNullValues = new List<PropertyInfo>();

                foreach (var prop in typeof(T).GetProperties())
                {
                    if (prop.GetValue(model) != null) notNullValues.Add(prop);
                }

                string filter = $"select * from {nameof(T)} where ";
                string operand = restricted ? "and" : "or";

                notNullValues.ForEach(x => filter += string.Concat(x.Name, " = ", x.PropertyType != typeof(int) ? "'" : "", x.GetValue(model), x.PropertyType != typeof(int) ? "' " : " ", operand, " "));
                filter = filter.Remove(filter.Length - operand.Length + 1);

                result.Content = await dbContext.Set<T>().FromSql(filter).ToListAsync();
                if (result.Content.Count > 0)result.AddResult(GetResponseCode(typeof(T).Name + "s_Found").GetCode(), GetResponseCode(typeof(T).Name + "s_Found").GetDescription(), ResultType.Success);
                else result.AddResult(GetResponseCode(typeof(T).Name + "s_NotFound").GetCode(), GetResponseCode(typeof(T).Name + "s_NotFound").GetDescription(), ResultType.Error);
            }
            catch (Exception)
            {
                result.AddResult(ResponseCode.Exception_Read.GetCode(), ResponseCode.Exception_Read.GetDescription(MethodBase.GetCurrentMethod().DeclaringType.Name), ResultType.Exception);
            }
            return result;
        }

        private ResponseCode GetResponseCode(string value)
        {
            return (ResponseCode)(Enum.Parse(typeof(ResponseCode), value));
        }
    }

}
