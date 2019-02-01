using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Joinup.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void Login()
        {
            //Login user
            app.Tap( x => x.Marked( "emailEntry" ) );
            app.EnterText( x => x.Marked( "emailEntry" ), "" );
            app.Tap( x => x.Marked( "passwordEntry" ) );
            app.EnterText( x => x.Marked( "passwordEntry" ), "" );
            app.Tap( x => x.Marked( "loginButton" ) );
        }
    }
}
