using HealthyWork.API.Contracts;
using HealthyWork.API.Contracts.Models;
using HealthyWork.API.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthyWork.API.Tests.Services.ValueServiceTest
{
    [TestClass]
    public class Create
    {
        private static ValueService valueService;
        private static HealthyDbContext dbContext;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            dbContext = new HealthyDbContext();
            valueService = new ValueService(dbContext);

        }

        [TestMethod]
        public async Task Create_Ok()
        {
            #region Arrange
            var hq = await dbContext.HeadQuarters.AddAsync(new HeadQuarters() { Id = Guid.NewGuid(), Name = "testHQ"});
            var room = await dbContext.Rooms.AddAsync(new Room() { Id = Guid.NewGuid(), Description = "roomTest", HeadQuartersId = hq.Entity.Id, Name = "testRoom" });
            //TODO: Cambiar por HQService y RoomService
            var value = new Value()
            {
                RoomId = room.Entity.Id,
                Level = PushLevel.Adecuado,
                Date = DateTime.Now,
                SensorValue = 20,
                Type = SensorType.Luz
            };
            #endregion
            #region Act
            var result = await valueService.Create(value);
            #endregion
            #region Assert
            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Results[0].Code == ResponseCode.Value_Created.GetCode());
            Assert.IsTrue(result.Results[0].Description == ResponseCode.Value_Created.GetDescription());
            Assert.IsTrue(result.Results[0].Type == ResultType.Success);
            #endregion
        }

        [TestMethod]
        public async Task Create_Fail()
        {
            #region Arrange
            var value = new Value()
            {
                RoomId = Guid.Empty,
                Level = PushLevel.Adecuado,
                Date = DateTime.Now,
                SensorValue = 20,
                Type = SensorType.Luz
            };
            #endregion
            #region Act
            var result = await valueService.Create(value);
            #endregion
            #region Assert
            Assert.IsNull(result.Content);
            Assert.IsTrue(result.Results[0].Code == ResponseCode.Exception_Create.GetCode());
            Assert.IsTrue(result.Results[0].Description == ResponseCode.Exception_Create.GetDescription());
            Assert.IsTrue(result.Results[0].Type == ResultType.Exception);
            #endregion
        }
    }
}
