using Microsoft.Extensions.Configuration;
using Moq;

namespace DVG.AP.CMS.CarInfo.Application.UnitTests.Mocks;
public static class ConfigurationMock
{
    private const string ConnectionString =
        "Server=172.16.0.7;Port=5432;User Id=team_cms_admin;Password=2yGyoG58iWx1a7qLdLJv;Database=autoportal_carinfo_phil;";

    public static Mock<IConfiguration> CreateConfiguration()
    {
        var mockConfSection = new Mock<IConfigurationSection>();
        mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "DBCarInfo")]).Returns(ConnectionString);

        var mockConfiguration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
        mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings")))
            .Returns(mockConfSection.Object);

        return mockConfiguration;
    }
}
