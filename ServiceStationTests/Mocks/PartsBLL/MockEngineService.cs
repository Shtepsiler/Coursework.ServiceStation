using Moq;
using PARTS.BLL.DTOs.Responses;
using PARTS.BLL.Services.Interaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationTests.Mocks.PartsBLL
{
    public class MockEngineService : Mock<IEngineService>
    {


        public async Task<MockEngineService> MockGetByIdAsync(EngineResponse result)
        {
            Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(result);

            return this;
        }





    }
}
