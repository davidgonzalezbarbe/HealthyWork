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
    public class Delete
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
        public async Task Delete_Ok()
        {//TODO: Revisar
            #region Arrange
            var hq = await dbContext.HeadQuarters.AddAsync(new HeadQuarters() { Id = Guid.NewGuid(), Name = "testHQ" });
            var room = await dbContext.Rooms.AddAsync(new Room() { Id = Guid.NewGuid(), Description = "roomTest", HeadQuartersId = hq.Entity.Id, Name = "testRoom" });
            var value = await dbContext.Values.AddAsync(new Value() { RoomId = room.Entity.Id, Level = PushLevel.Adecuado, Date = DateTime.Now, SensorValue = 20, Type = SensorType.Luz });
            await dbContext.SaveChangesAsync();
            #endregion
            #region Act
            var result = await valueService.Read(value.Entity.Id);
            #endregion
            #region Assert
            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Results[0].Code == ResponseCode.Value_Found.GetCode());
            Assert.IsTrue(result.Results[0].Description == ResponseCode.Value_Found.GetDescription());
            Assert.IsTrue(result.Results[0].Type == ResultType.Success);
            #endregion
        }

        [TestMethod]
        public async Task Delete_Fail()
        {
            #region Arrange

            #endregion
            #region Act
            var result = await valueService.Read(Guid.NewGuid());
            #endregion
            #region Assert
            Assert.IsNull(result.Content);
            Assert.IsTrue(result.Results[0].Code == ResponseCode.Value_NotFound.GetCode());
            Assert.IsTrue(result.Results[0].Description == ResponseCode.Value_NotFound.GetDescription());
            Assert.IsTrue(result.Results[0].Type == ResultType.Error);
            #endregion
        }
    }
}
