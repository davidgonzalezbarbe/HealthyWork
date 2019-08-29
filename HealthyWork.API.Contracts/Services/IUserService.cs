using HealthyWork.API.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyWork.API.Contracts.Services
{
    public interface IUserService
    {
        ResultData<User> Login(User model);
    }
}
