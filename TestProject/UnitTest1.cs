using Microsoft.Build.Framework;
using Moq;
using NuGet.Configuration;
using NuGet.Protocol.Core.Types;
using System.Xml;

namespace TestProject
{

    [TestClass]
    public class UnitTest1
    {
        private Mock<IBuildEngine> buildEngine;
        private List<BuildErrorEventArgs> errors;

        [TestMethod]
        public void TestMethodCSProj()
        {
            XmlDocument xdDoc = new XmlDocument();
            xdDoc.Load(@"C:\Users\amedeo.gautiero\source\repos\damgau.msbuild.tasks\ClassLibraryTestCase\ClassLibraryTestCase.csproj");
        }

        [TestMethod]
        public void TestMethod1()
        {

            try
            {
                string path = @"C:\Users\amedeo.gautiero\.nuget\packages\damgau.msbuild.tasks\0.0.0.14-prerelease\build\..\assets";
                string fp = System.IO.Path.GetFullPath(path);
            }
            catch (Exception ex) 
            { 
            }

            buildEngine = new Mock<IBuildEngine>();
            errors = new List<BuildErrorEventArgs>();
            buildEngine.Setup(x => x.LogErrorEvent(It.IsAny<BuildErrorEventArgs>())).Callback<BuildErrorEventArgs>(e => errors.Add(e));

            var nugetPushTask = new damgau.msbuild.tasks.nuget.NugetPush();
            nugetPushTask.BuildEngine = buildEngine.Object;

            

            nugetPushTask.Execute();
        }

        
    }
}