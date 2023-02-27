using System.Threading;
using NUnit.Framework;

[assembly: Apartment(ApartmentState.STA)]

namespace ReSharperPlugin.GenericsInlayHints.Tests
{
    // [ZoneDefinition]
    // public class MyTestPluginTestEnvironmentZone : ITestsEnvZone, IRequire<PsiFeatureTestZone>, IRequire<IMyTestPluginZone> { }
//
//     [ZoneMarker]
//     public class ZoneMarker : IRequire<ICodeEditingZone>, IRequire<ILanguageCSharpZone>, IRequire<MyTestPluginTestEnvironmentZone> { }
//
//     [SetUpFixture]
//     public class MyTestPluginTestsAssembly : ExtensionTestEnvironmentAssembly<MyTestPluginTestEnvironmentZone> { }
}
