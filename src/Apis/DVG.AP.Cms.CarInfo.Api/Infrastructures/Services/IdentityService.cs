namespace DVG.AP.Cms.CarInfo.Api.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        /// <summary>
        /// Get Current Userid
        /// </summary>
        /// <returns>The Id of Current User</returns>
        public int GetUserIdentity()
        {
            // Fake Data Return UserId Of SonDT with Id 1
            return 1;
        }
    }
}