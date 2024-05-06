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
    public class MockSubModelService : Mock<ISubModelService>
    {

        public async Task<MockSubModelService> MockGetByIdAsync(SubModelResponse result)
        {
            Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(result);

            return this;
        }






    }
}
