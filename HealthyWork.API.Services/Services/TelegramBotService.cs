using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using HealthyWork.API.Contracts.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace HealthyWork.API.Services.Services
{
    public class TelegramBotService : ITelegramBotService
    {

        private readonly string Token = "650879446:AAGCcKo1HhieHqWUjAm9b_cbmYfJdM12uUA";
        private readonly TelegramBotClient bot;
        private bool step1 = true;
        private bool step2 = false;
        private bool step3 = false;
        private readonly List<List<string>> OkMessages = new List<List<string>>
        {
            new List<string>
            {
                "Vale, escoja su sede de una de las existentes:",
                "De acuerdo, para registrarse introduzca el nombre de la sala de la que quiere la información",
                "Bien, se le notificará cuando haya una alerta de los valores predeterminados por los sensores de la sala {sala}"
            },
            new List<string>
            {

            }
        };
        private readonly List<List<string>> ErrorMessages = new List<List<string>>
        {
            new List<string>//Register messages 
            {
                "Lo siento, la sede que ha escogido no existe",
                "Lo siento, la sala que ha escogido no existe",
                "Lo siento, ha sido imposible registrarle para avisarle de las alertas, intentelo de nuevo más tarde",
                "Lo siento, usted ya está registrado en esa sala"
            },
            new List<string>// Get messages
            {

            }
        };


        public TelegramBotService()
        {

            bot = new TelegramBotClient(Token);
        }

        public void Run()
        {
            Configure();
            bot.StartReceiving();
        }

        private void Configure()
        {
            bot.OnMessage += async (x, y) =>
            {
                using (var _dbContext = new HealthyDbContext())
                {
                    var text = y.Message.Text;
                    if (text.Coincide() && step1)
                    {
                        if (text.GetOption() == 0)
                        {
                            var correct = await DoOptionRegister(0, y, _dbContext);
                            step1 = !correct;
                            step2 = correct;
                            step3 = false;
                        }
                        if ((int)text.GetOption() == 1)
                        {
                            //Hacer Get
                        }

                    }
                    else if (step2)
                    {
                        var correct = await DoOptionRegister(1, y, _dbContext, y.Message.Text);
                        step1 = !correct;
                        step2 = false;
                        step3 = correct;
                    }
                    else if (step3)
                    {
                        var correct = await DoOptionRegister(2, y, _dbContext, "", y.Message.Text);
                        step1 = true;
                        step2 = false;
                        step3 = false;
                    }
                    else await bot.SendTextMessageAsync(y.Message.Chat.Id, "Lo siento, no ha escogido una opción viable");
                }

            };
        }

        public void Stop()
        {
            bot.StopReceiving();
        }

        private async Task<bool> SaveChat(long chatId, Guid roomId, HealthyDbContext _dbContext)
        {
            try
            {
                if (roomId == Guid.Empty) return false;

                var newPush = new TelegramPush
                {
                    Id = Guid.NewGuid(),
                    ChatId = chatId,
                    RoomId = roomId
                };

                var result = await _dbContext.TelegramPushes.AddAsync(newPush);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        private async Task<Guid> SeachRoom(string roomName, HealthyDbContext _dbContext)
        {
            var room = new Room() { Name = roomName };
            var roomFinded = await _dbContext.Rooms.FirstOrDefaultAsync(x => x.Name.ToLower() == roomName.ToLower());

            return roomFinded == null ? Guid.Empty : roomFinded.Id;
        }

        private async Task<Guid> SeachHQ(string hqName, HealthyDbContext _dbContext)
        {
            var hq = new HeadQuarters() { Name = hqName };
            var hqFinded = await _dbContext.HeadQuarters.FirstOrDefaultAsync(x => x.Name.ToLower() == hqName.ToLower());

            return hqFinded == null ? Guid.Empty : hqFinded.Id;
        }

        private async Task<bool> DoOptionRegister(int step, MessageEventArgs y, HealthyDbContext _dbContext, string hqName = "", string roomName = "")
        {
            switch (step)
            {
                case 0:
                    await bot.SendTextMessageAsync(y.Message.Chat.Id, OkMessages[0][step]);
                    var hqs = (await _dbContext.HeadQuarters.ToListAsync()).Select(x => x.Name).ToList();
                    await bot.SendTextMessageAsync(y.Message.Chat.Id, hqs.ListToString());
                    return true;

                case 1:

                    var hq = await SeachHQ(hqName, _dbContext);
                    if (hq == Guid.Empty)
                    {
                        await bot.SendTextMessageAsync(y.Message.Chat.Id, ErrorMessages[0][0]);
                        return false;
                    }

                    await bot.SendTextMessageAsync(y.Message.Chat.Id, OkMessages[0][step]);
                    var rooms = (await _dbContext.Rooms.Where(x => x.HeadQuartersId == hq).ToListAsync()).Select(x => x.Name + " => " + x.Description).ToList();
                    var hqRooms = rooms.ListToString();
                    await bot.SendTextMessageAsync(y.Message.Chat.Id, string.IsNullOrEmpty(hqRooms) ? "Lo siento, actualmente no hay salas en esta sede" : hqRooms);
                    return string.IsNullOrEmpty(hqRooms) ? false : true;

                case 2:
                    var room = await SeachRoom(roomName, _dbContext);
                    if (room == Guid.Empty)
                    {
                        await bot.SendTextMessageAsync(y.Message.Chat.Id, ErrorMessages[0][1]);
                        return false;
                    }
                    if (await IsNotRegistered(room, y.Message.Chat.Id, _dbContext))
                    {
                        var result = await SaveChat(y.Message.Chat.Id, room, _dbContext);
                        await bot.SendTextMessageAsync(y.Message.Chat.Id, result ? OkMessages[0][step].Replace("{sala}",roomName) : ErrorMessages[0][2]);
                        return result;
                    }
                    else
                    {
                        await bot.SendTextMessageAsync(y.Message.Chat.Id, ErrorMessages[0][3]);
                        return false;
                    }


            }
            return false;

        }

        private async Task<bool> IsNotRegistered(Guid room, long id, HealthyDbContext _dbContext)
        {
            var exists = await _dbContext.TelegramPushes.CountAsync(x => x.ChatId == id && x.RoomId == room);
            return exists > 0 ? false : true;

        }

        //private async Task<bool> DoOption2(string hqName, int option, MessageEventArgs y)
        //{
        //    //Segundo mensaje
        //    switch (option)
        //    {
        //        //Register
        //        case 0:
        //            var hq = (await hqService.ReadFiltered(new HeadQuarters() { Name = hqName }, false)).Content.First();
        //            if (hq == null)
        //            {
        //                await bot.SendTextMessageAsync(y.Message.Chat.Id, ErrorMessages[option][0]);
        //                return false;
        //            }
        //            else
        //            {
        //                await bot.SendTextMessageAsync(y.Message.Chat.Id, OkMessages[option][0]);
        //                var rooms = (await roomService.ReadFiltered(new Room { HeadQuartersId = hq.Id }, false)).Content.Select(x => x.Name + " => " + x.Description).ToList();
        //                await bot.SendTextMessageAsync(y.Message.Chat.Id, rooms.ListToString());
        //                return true;
        //            }

        //        case 1:
        //            //Get
        //            break;
        //    }
        //}

    }

    public enum TelegramCommands
    {
        register = 0,
        get = 1,
    }

    public static class ExtensionTelegramMethods
    {
        public static List<string> ToStringList()
        {
            var enums = Enum.GetValues(typeof(TelegramCommands)).Cast<TelegramCommands>().ToList();
            return enums.Select(x => Enum.GetName(x.GetType(), x)).ToList();
        }

        public static bool Coincide(this string cadena)
        {
            return ToStringList().Find(x => x.Contains(cadena)) != null ? true : false;
        }

        public static TelegramCommands GetOption(this string cadena)
        {
            return (TelegramCommands)Enum.Parse(typeof(TelegramCommands), ToStringList().Find(x => x.Contains(cadena)));
        }

        public static string ListToString(this List<string> lista)
        {
            string sedes = "";
            lista.ForEach(x => sedes += x + "\n");
            return sedes;
        }
    }
}
