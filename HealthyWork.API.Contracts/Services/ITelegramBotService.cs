using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyWork.API.Contracts.Services
{
    public interface ITelegramBotService
    {
        void Run();
        void Stop();
    }
}
