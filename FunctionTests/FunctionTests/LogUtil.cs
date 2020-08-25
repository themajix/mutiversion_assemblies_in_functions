using System.Linq;
using System.Runtime.Loader;
using Microsoft.Extensions.Logging;

namespace FunctionTests
{
    public static class LogUtil
    {
        public static void LogLoadedAssemblies(ILogger log, string assemblyName)
        {
            foreach (var context in AssemblyLoadContext.All)
            {
                var assemblies = context.Assemblies
                    .Where(a => a.FullName != null && a.FullName.Contains(assemblyName))
                    .Select(a => $"{a.FullName}; LOCATION: {a.Location}");

                log.LogWarning($"AssemblyLoadContext: {context}");

                var assembliesArray = assemblies as string[] ?? assemblies.ToArray();

                log.LogWarning(assembliesArray.Any()
                    ? string.Join('\n', assembliesArray)
                    : $"{assemblyName} has not been loaded in this AssemblyLoadContext.");

                log.LogWarning("==========================================================");
            }
        }
    }
}