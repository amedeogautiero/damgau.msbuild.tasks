using Microsoft.Build.Framework;
using Moq;
using NuGet.Configuration;
using NuGet.Protocol.Core.Types;

namespace TestProject
{

    [TestClass]
    public class UnitTest1
    {
        private Mock<IBuildEngine> buildEngine;
        private List<BuildErrorEventArgs> errors;

        [TestMethod]
        public void TestMethod1()
        {
            buildEngine = new Mock<IBuildEngine>();
            errors = new List<BuildErrorEventArgs>();
            buildEngine.Setup(x => x.LogErrorEvent(It.IsAny<BuildErrorEventArgs>())).Callback<BuildErrorEventArgs>(e => errors.Add(e));

            var nugetPushTask = new damgau.msbuild.tasks.nuget.NugetPush();
            nugetPushTask.BuildEngine = buildEngine.Object;

            

            nugetPushTask.Execute();
        }

        
    }
}