using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using HealthyWork.API.Contracts.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthyWork.API.Services.Services
{
    public class UserService: BaseService<User>, IService<User>, IUserService
    {
        private readonly HealthyDbContext dbContext;

        public UserService(HealthyDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultData<User>> Create(User model) => await CreateAsync(model);


        public async Task<ResultData<User>> Delete(Guid modelId) => await DeleteAsync(modelId);


        public async Task<ResultData<User>> Read(Guid modelId) => await ReadAsync(modelId);


        public async Task<ResultData<List<User>>> ReadAll() => await ReadAllAsync();

        public async Task<ResultData<User>> Update(User model) => await UpdateAsync(model, model.Id);

        public async Task<ResultData<List<User>>> ReadFiltered(User model, bool restricted) => await ReadFilteredAsync(model, restricted);

        public ResultData<string> EncryptData(User model)
        {
            ResultData<string> result = new ResultData<string>() { Content = string.Empty };

            try
            {
                var objectSerialized = JsonConvert.SerializeObject(model);
                byte[] encryted = Encoding.Unicode.GetBytes(objectSerialized);
                result.Content = Convert.ToBase64String(encryted);
                result.AddResult(ResponseCode.Object_Encrypted.GetCode(), ResponseCode.Object_Encrypted.GetDescription(), ResultType.Success);
            }
            catch (Exception)
            {
                result.AddResult(ResponseCode.Object_Encryption.GetCode(), ResponseCode.Object_Encryption.GetDescription(), ResultType.Exception);
            }
            return result;
        }

        public ResultData<User> DecryptData(string code)
        {
            ResultData<User> result = new ResultData<User>() { Content = null };

            try
            {
                byte[] decrypted = Convert.FromBase64String(code);
                var objectSerialized = Encoding.Unicode.GetString(decrypted);
                result.Content = JsonConvert.DeserializeObject<User>(objectSerialized);
                result.AddResult(ResponseCode.Object_Desencrypted.GetCode(), ResponseCode.Object_Desencrypted.GetDescription(), ResultType.Success);
            }
            catch (Exception)
            {
                result.AddResult(ResponseCode.Object_Encryption.GetCode(), ResponseCode.Object_Encryption.GetDescription(), ResultType.Exception);
            }
            return result;
        }
    }
}
