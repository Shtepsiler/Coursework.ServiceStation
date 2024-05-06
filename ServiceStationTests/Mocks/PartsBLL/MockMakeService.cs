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
    public class MockMakeService : Mock<IMakeService>
    {
        public async Task<MockMakeService> MockGetByIdAsync(MakeResponse result)
        {
            Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(result);

            return this;
        }







    }
}
