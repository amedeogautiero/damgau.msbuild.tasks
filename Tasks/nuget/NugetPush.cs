using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NuGet;
using System.Linq;

namespace damgau.msbuild.tasks.nuget
{
    //https://devblogs.microsoft.com/nuget/play-with-packages/
    public class NugetPush : Task
    {
        public override bool Execute()
        {
            Log.LogMessage(MessageImportance.High, "NugetPush start");

            var localRepo = PackageRepositoryFactory.Default.CreateRepository(@"C:\Users\amedeo.gautiero\source\repos\OPC-UA\OPCUAInterfaces\bin\Release");
            var package = localRepo.FindPackagesById("OPCUAInterfaces").First();
            var packageFile = new FileInfo(@"C:\Users\amedeo.gautiero\source\repos\OPC-UA\OPCUAInterfaces\bin\Release\OPCUAInterfaces.0.0.0.7-prerelease.nupkg");
            var size = packageFile.Length;
            var ps = new PackageServer("https://nuget.pkg.github.com/amedeogautiero/index.json", "userAgent");
            ps.PushPackage(ApiKey, package, size, 1800, false);

            Log.LogMessage(MessageImportance.High, "NugetPush end");
            return true;
        }

        [Required]
        public string Source { get; set; }

        public string ApiKey { get; set; }
    }
}
