using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

using System.Linq;
using NuGet.Packaging;
using NuGet.Common;
using NuGet.Configuration;
using System.Collections.Generic;
using NuGet.Protocol.Core.Types;

namespace damgau.msbuild.tasks.nuget
{
    //https://learn.microsoft.com/en-us/visualstudio/msbuild/tutorial-custom-task-code-generation?view=vs-2022
    //https://devblogs.microsoft.com/nuget/play-with-packages/
    public class NugetPush : Task
    {
        public override bool Execute()
        {
            Log.LogMessage(MessageImportance.High, "NugetPush start");

            //var localRepo = NuGet.PackageRepositoryFactory.Default.CreateRepository(@"C:\Users\amedeo.gautiero\source\repos\OPC-UA\OPCUAInterfaces\bin\Release");
            //var package = localRepo.FindPackagesById("OPCUAInterfaces").First();
            //var packageFile = new FileInfo(@"C:\Users\amedeo.gautiero\source\repos\OPC-UA\OPCUAInterfaces\bin\Release\OPCUAInterfaces.0.0.0.7-prerelease.nupkg");
            //var size = packageFile.Length;
            //var ps = new NuGet.PackageServer("https://nuget.pkg.github.com/amedeogautiero/index.json", "userAgent");
            //ps.PushPackage(ApiKey, package, size, 1800, false);
            
            //string nugetRepositoryUrl = "https://nuget.pkg.github.com/amedeogautiero/index.json";
            //string nugetRepositoryUrl = "https://nuget.pkg.github.com";
            //string packagePath = "";
            //string apiKey = "";
            //System.Threading.Tasks.Task.Run(async () =>
            //{
            //    await NugetPush.UploadPackage(nugetRepositoryUrl, packagePath, apiKey);
            //});



            var location = this.GetType().Assembly.Location;
            Log.LogMessage(MessageImportance.High, $"NugetPush location: {location}");
            //Log.LogMessage(MessageImportance.High, $"NugetPush HostObject: {base.HostObject.GetType().FullName}");
            //Log.LogMessage(MessageImportance.High, $"NugetPush HelpKeywordPrefix: {base.HelpKeywordPrefix}");
            //Log.LogMessage(MessageImportance.High, $"NugetPush BuildEngine: {base.BuildEngine != null}");
            //Log.LogMessage(MessageImportance.High, $"NugetPush BuildEngine2: {base.BuildEngine2 != null}");
            //Log.LogMessage(MessageImportance.High, $"NugetPush BuildEngine3: {base.BuildEngine3 != null}");
            //Log.LogMessage(MessageImportance.High, $"NugetPush BuildEngine4: {base.BuildEngine4 != null}");
            //Log.LogMessage(MessageImportance.High, $"NugetPush BuildEngine5: {base.BuildEngine5 != null}");
            //Log.LogMessage(MessageImportance.High, $"NugetPush BuildEngine6: {base.BuildEngine6 != null}");
            //Log.LogMessage(MessageImportance.High, $"NugetPush BuildEngine7: {base.BuildEngine7 != null}");
            //Log.LogMessage(MessageImportance.High, $"NugetPush BuildEngine8: {base.BuildEngine8 != null}");
            //Log.LogMessage(MessageImportance.High, $"NugetPush BuildEngine9: {base.BuildEngine9 != null}");

            Log.LogMessage(MessageImportance.High, "NugetPush end");

            return true;
        }

        public static async System.Threading.Tasks.Task UploadPackage(string nugetRepositoryUrl, string packagePath, string apiKey)
        {
            List<Lazy<INuGetResourceProvider>> providers = new List<Lazy<INuGetResourceProvider>>();
            providers.AddRange(Repository.Provider.GetCoreV3());
            PackageSource packageSource = new PackageSource(nugetRepositoryUrl);

            try
            {
                SourceRepository sourceRepository2 = new SourceRepository(packageSource, providers);
            }
            catch (Exception exc)
            { 
            }

            SourceRepository sourceRepository = new SourceRepository(packageSource, providers);
            List<string> packagePaths = new List<string>()
            {
                packagePath
            };
            using (var sourceCacheContext = new SourceCacheContext())
            {
                PackageUpdateResource uploadResource = await sourceRepository.GetResourceAsync<PackageUpdateResource>();
                //await uploadResource.Push(packagePath, null, 480, false, (param) => apiKey, null, NullLogger.Instance);
                //string packagePath, 
                //string symbolSource, 
                //int timeoutInSecond, 
                //bool disableBuffering, 
                //Func<string, string> getApiKey, 
                //Func<string, string> getSymbolApiKey, 
                //bool noServiceEndpoint, 
                //ILogger log)
                await uploadResource.Push(packagePath, null, 10,true, (t) => "", null, false, NullLogger.Instance);


            }
        }

        [Required]
        public string Source { get; set; }

        public string ApiKey { get; set; }
    }
}
