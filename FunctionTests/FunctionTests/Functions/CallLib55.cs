using System;
using System.Threading.Tasks;
using System.Web.Http;
using Lib55;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace FunctionTests.Functions
{
    /// <summary>
    /// this function calls a method in Lib55
    /// the called method does not return any values
    /// </summary>
    public static class CallLib55
    {
        [FunctionName("CallLib55")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            const string assemblyName = "Microsoft.IdentityModel.Protocols.OpenIdConnect";

            try
            {
                log.LogWarning("===== BEFORE ANY CALL ====================================================");
                LogUtil.LogLoadedAssemblies(log, assemblyName);

                log.LogInformation("**** about to make call to c55...");
                var c55 = new Lib55Class();
                await c55.ReturnNothing();
                log.LogInformation("**** c55 was called successfully");

                log.LogWarning("===== AFTER FIRST CALL ====================================================");
                LogUtil.LogLoadedAssemblies(log, assemblyName);

                return new OkResult();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                log.LogWarning("===== AFTER THROWING THE EXCEPTION ====================================================");
                LogUtil.LogLoadedAssemblies(log, assemblyName);
                return new InternalServerErrorResult();
            }
        }
    }
}
