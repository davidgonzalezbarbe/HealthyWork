using HealthyWork.API.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthyWork.API.Contracts.Services
{
    public interface IUserService
    {
        ResultData<string> EncryptData(User model);
        ResultData<User> DecryptData(string code);
        

    }
}
