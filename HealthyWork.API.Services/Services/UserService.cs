using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using HealthyWork.API.Contracts.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HealthyWork.API.Services.Services
{
    public class UserService : BaseService<User>, IService<User>, IUserService
    {
        private readonly HealthyDbContext dbContext;

        public UserService(HealthyDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultData<User>> Create(User model) => await CreateAsync(PrepareUser(model));


        public async Task<ResultData<User>> Delete(Guid modelId) => await DeleteAsync(modelId);


        public async Task<ResultData<User>> Read(Guid modelId) => await ReadAsync(modelId);


        public async Task<ResultData<List<User>>> ReadAll() => await ReadAllAsync();
        

        public async Task<ResultData<User>> Update(User model) => await UpdateAsync(PrepareUser(model), model.Id);

        public async Task<ResultData<List<User>>> ReadFiltered(User model, bool restricted) => await ReadFilteredAsync(model, restricted);

        private User PrepareUser(User model)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] encrypted = Encoding.ASCII.GetBytes(model.Password);
            model.Password = Convert.ToBase64String(md5.ComputeHash(encrypted));
            return model;
        }

        public ResultData<User> Login(User model)
        {
            ResultData<User> result = new ResultData<User>() { Content = null };

            try
            {
                var newModel = PrepareUser(model);
                var exist = dbContext.Users.FirstOrDefault(x => x.Email == newModel.Email && x.Password == newModel.Password && x.IsActive);
                if (exist != null)
                {
                    result.Content = exist;
                    result.AddResult(ResponseCode.Users_Found.GetCode(), ResponseCode.Users_Found.GetDescription(), ResultType.Success);
                }
                else
                {
                    result.AddResult(ResponseCode.Users_NotFound.GetCode(), ResponseCode.Users_NotFound.GetDescription(), ResultType.Error);
                }
            }
            catch (Exception)
            {
                result.AddResult(ResponseCode.Exception_Read.GetCode(), ResponseCode.Exception_Read.GetDescription(), ResultType.Exception);

            }
            return result;
        }

    }
}
