using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Lib56
{
    public class Lib56Class
    {
        public async Task ReturnNothing()
        {
            var metadataAddress =
                "https://justformajidstests.b2clogin.com/justformajidstests.onmicrosoft.com/B2C_1_DUMMY/v2.0/.well-known/openid-configuration";

            var configRetriever = new HttpDocumentRetriever { RequireHttps = true };

            var configurationManager =
                new ConfigurationManager<OpenIdConnectConfiguration>(
                    metadataAddress,
                    new OpenIdConnectConfigurationRetriever(),
                    configRetriever
                );

            var config = await configurationManager.GetConfigurationAsync(CancellationToken.None)
                .ConfigureAwait(false);

            var signingKeys = config.SigningKeys;
        }
        public async Task<ICollection<SecurityKey>> ReturnSomething()
        {
            var metadataAddress =
                "https://justformajidstests.b2clogin.com/justformajidstests.onmicrosoft.com/B2C_1_DUMMY/v2.0/.well-known/openid-configuration";

            var configRetriever = new HttpDocumentRetriever { RequireHttps = true };

            var configurationManager =
                new ConfigurationManager<OpenIdConnectConfiguration>(
                    metadataAddress,
                    new OpenIdConnectConfigurationRetriever(),
                    configRetriever
                );

            var config = await configurationManager.GetConfigurationAsync(CancellationToken.None)
                .ConfigureAwait(false);

            var signingKeys = config.SigningKeys;
            return signingKeys;
        }
    }
}
