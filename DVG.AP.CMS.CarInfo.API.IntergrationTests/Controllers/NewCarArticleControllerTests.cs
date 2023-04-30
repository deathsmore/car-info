using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using System.Net;
using DVG.AP.CMS.CarInfo.API.IntergrationTests.Bases;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DVG.AP.CMS.CarInfo.API.IntergrationTests.Controllers
{
    public class NewCarArticleControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public NewCarArticleControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
    }
}
