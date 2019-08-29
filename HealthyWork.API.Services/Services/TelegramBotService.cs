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

        #region Private declarations
        private readonly string Token = "650879446:AAGCcKo1HhieHqWUjAm9b_cbmYfJdM12uUA";
        private readonly TelegramBotClient bot;
        private bool step1 = false;
        private bool step2 = false;
        private bool step3 = false;
        private int option = -1;
        private readonly List<string> OkMessages = new List<string>
        {
            "Vale, escoja su sede de una de las existentes:",
            "De acuerdo, para registrarse introduzca el nombre de la sala de la que quiere la información",
            "Bien, se le notificará cuando haya una alerta de los valores predeterminados por los sensores de la sala {sala}",
            "Vale, seleccione cual de las salas en las que está inscrito desea quitar:",
            "Muy bien, se va a desuscribir de la sala {sala}, está seguro? si/no",
            "¡Hecho! Ya no recibirá más notificaciones de los sensores de esta sala"
        };
        private readonly List<string> ErrorMessages = new List<string>
        {
            "Lo siento, la sede que ha escogido no existe",
            "Lo siento, la sala que ha escogido no existe",
            "Lo siento, ha sido imposible registrarle para avisarle de las alertas, intentelo de nuevo más tarde",
            "Lo siento, usted ya está registrado en esa sala",
            ""//TODO: Acabar error messages de UnSuscribe, y acabar unsuscribe
        };
        private readonly string AlertTemplate = "Se ha {estado} el sensor de {sensor} con un valor de {valor} en la sala {sala} de la sede {sede}";
        #endregion

        #region Class Constructor
        public TelegramBotService()
        {

            bot = new TelegramBotClient(Token);
        }
        #endregion

        #region Public Methods
        public void Run()
        {
            Configure();
            bot.StartReceiving();
        }
        public void Stop()
        {
            bot.StopReceiving();
        }
        public async void Send(Value value)
        {
            using (var dbContext = new HealthyDbContext())
            {
                var room = await dbContext.Rooms.FindAsync(value.RoomId);
                var hq = await dbContext.HeadQuarters.FindAsync(room.HeadQuartersId);
                var peopleinRoom = await dbContext.TelegramPushes.Where(x => x.RoomId == room.Id).Select(x => x.ChatId).ToListAsync();
                var message = AlertTemplate
                .Replace("{estado}", Enum.GetName(typeof(PushLevel), value.Level)).ToLower()
                .Replace("{valor}", value.SensorValue.ToString())
                .Replace("{sensor}", Enum.GetName(typeof(SensorType), value.Type))
                .Replace("{sala}", room.Name)
                .Replace("{sede}", hq.Name);
                peopleinRoom.ForEach(async x => await bot.SendTextMessageAsync(x, message));
            }
        }
        #endregion

        #region Private Methods
        private void Configure()
        {
            bot.OnMessage += async (x, y) =>
            {
                using (var _dbContext = new HealthyDbContext())
                {
                    var text = y.Message.Text;

                    if (text == "!register")
                    {
                        option = 0;
                        step1 = true;
                        step2 = false;
                        step3 = false;
                    }
                    else if (text == "!unregister")
                    {
                        option = 1;
                        step1 = true;
                        step2 = false;
                        step3 = false;
                    }

                    if (option > -1)
                    {
                        if (step1)
                        {
                            var correct = option == 0  ? await DoOptionRegister(0, y, _dbContext) : await DoOptionUnRegister(0,y,_dbContext);
                            step1 = !correct;
                            step2 = correct;
                            step3 = false;
                            option = !correct ? -1 : option;
                        }
                        else if (step2)
                        {
                            var correct = option == 0 ? await DoOptionRegister(1, y, _dbContext) : await DoOptionUnRegister(1, y, _dbContext);
                            step1 = !correct;
                            step2 = false;
                            step3 = correct;
                            option = !correct ? -1 : option;
                        }
                        else if (step3)
                        {
                            var correct = option == 0 ? await DoOptionRegister(2, y, _dbContext) : await DoOptionUnRegister(2, y, _dbContext);
                            step1 = true;
                            step2 = false;
                            step3 = false;
                            option = -1;
                        } 
                    }
                    else await bot.SendTextMessageAsync(y.Message.Chat.Id, "Lo siento, no ha escogido una opción viable");
                }

            };
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
        private async Task<bool> IsNotRegistered(Guid room, long id, HealthyDbContext _dbContext)
        {
            var exists = await _dbContext.TelegramPushes.CountAsync(x => x.ChatId == id && x.RoomId == room);
            return exists > 0 ? false : true;

        }
        private async Task<bool> DoOptionRegister(int step, MessageEventArgs y, HealthyDbContext _dbContext)
        {
            switch (step)
            {
                case 0:
                    await bot.SendTextMessageAsync(y.Message.Chat.Id, OkMessages[step]);
                    var hqs = (await _dbContext.HeadQuarters.ToListAsync()).Select(x => x.Name).ToList();
                    await bot.SendTextMessageAsync(y.Message.Chat.Id, hqs.ListToString());
                    return true;

                case 1:

                    var hq = await SeachHQ(y.Message.Text, _dbContext);
                    if (hq == Guid.Empty)
                    {
                        await bot.SendTextMessageAsync(y.Message.Chat.Id, ErrorMessages[0]);
                        return false;
                    }

                    await bot.SendTextMessageAsync(y.Message.Chat.Id, OkMessages[step]);
                    var rooms = (await _dbContext.Rooms.Where(x => x.HeadQuartersId == hq).ToListAsync()).Select(x => x.Name + " => " + x.Description).ToList();
                    var hqRooms = rooms.ListToString();
                    await bot.SendTextMessageAsync(y.Message.Chat.Id, string.IsNullOrEmpty(hqRooms) ? "Lo siento, actualmente no hay salas en esta sede" : hqRooms);
                    return string.IsNullOrEmpty(hqRooms) ? false : true;

                case 2:
                    var room = await SeachRoom(y.Message.Text, _dbContext);
                    if (room == Guid.Empty)
                    {
                        await bot.SendTextMessageAsync(y.Message.Chat.Id, ErrorMessages[1]);
                        return false;
                    }
                    if (await IsNotRegistered(room, y.Message.Chat.Id, _dbContext))
                    {
                        var result = await SaveChat(y.Message.Chat.Id, room, _dbContext);
                        await bot.SendTextMessageAsync(y.Message.Chat.Id, result ? OkMessages[step].Replace("{sala}", y.Message.Text) : ErrorMessages[2]);
                        return result;
                    }
                    else
                    {
                        await bot.SendTextMessageAsync(y.Message.Chat.Id, ErrorMessages[3]);
                        return false;
                    }


            }
            return false;

        }
        private async Task<bool> DoOptionUnRegister(int step, MessageEventArgs y, HealthyDbContext _dbContext)
        {
            switch (step)
            {
                case 0:


                    break;
                case 1:

                    break;
                case 2:

                    break;
            }
            return false; 
        }
        #endregion

    }
    public static class ExtensionTelegramMethods
    {

        public static string ListToString(this List<string> lista)
        {
            string sedes = "";
            lista.ForEach(x => sedes += x + "\n");
            return sedes;
        }
    }
}
