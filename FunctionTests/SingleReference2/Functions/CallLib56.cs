using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using Lib56;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace SingleReference2.Functions
{
    /// <summary>
    /// this function calls a method in Lib56
    /// the called method does not return any values
    /// </summary>
    public static class CallLib56
    {
        [FunctionName("CallLib56")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            const string assemblyName = "Microsoft.IdentityModel.Protocols.OpenIdConnect";

            try
            {
                log.LogWarning("===== BEFORE ANY CALL ====================================================");
                LogUtil.LogLoadedAssemblies(log, assemblyName);

                log.LogInformation("**** about to make call to c56...");
                var c56 = new Lib56Class();
                await c56.ReturnNothing();
                log.LogInformation("**** c56 was called successfully");

                log.LogWarning("===== AFTER FIRST CALL ====================================================");
                LogUtil.LogLoadedAssemblies(log, assemblyName);

                return new OkResult();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                log.LogWarning("===== AFTER THROWING THE EXCEPTION ====================================================");
                LogUtil.LogLoadedAssemblies(log, assemblyName);

                // READ IMPORTANT NOTE ABOUT THE FOLLOWING LINE OF CODE IN README.MD
                //the class OpenIdConnectConfiguration resides in Microsoft.IdentityModel.Protocols.OpenIdConnect
                var assembly = Assembly.GetAssembly(typeof(OpenIdConnectConfiguration));
                log.LogWarning(
                    assembly != null
                        ? $"{assembly.FullName} is loaded in the AssemblyLoadContext: #{assembly.HostContext}"
                        : "Microsoft.IdentityModel.Protocols.OpenIdConnect is not loaded.");

                return new InternalServerErrorResult();
            }
        }
    }
}
