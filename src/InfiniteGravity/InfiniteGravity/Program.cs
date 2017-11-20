#region Using Statements

using System;
using System.IO;
using InfiniteGravity.Configuration;
using Newtonsoft.Json;

#if MONOMAC
using MonoMac.AppKit;
using MonoMac.Foundation;
#elif __IOS__ || __TVOS__
using Foundation;
using UIKit;
#endif

#endregion

namespace InfiniteGravity {
#if __IOS__ || __TVOS__
    [Register("AppDelegate")]
    class Program : UIApplicationDelegate
#else
    static class Program
#endif
    {

    public const string ConfigFileName = "game_config.json";

    private static NGame game;

    internal static void RunGame() {
    GameConfiguration config = null;
        // load config file
        if (File.Exists(ConfigFileName)) {
        config = JsonConvert.DeserializeObject<GameConfiguration>(File.ReadAllText(ConfigFileName));
    }
    else {
    config = new GameConfiguration();
    File.WriteAllText(ConfigFileName, JsonConvert.SerializeObject(config, Formatting.Indented));
    }
    config.bind(ConfigFileName);
    var context = new GameContext(config);
    game = new NGame(context);
    game.Run();
#if !__IOS__ && !__TVOS__
    game.Dispose();
#endif
}

/// <summary>
/// The main entry point for the application.
/// </summary>
#if !MONOMAC && !__IOS__ && !__TVOS__
[STAThread]
#endif
static void Main(string[] args) {
#if MONOMAC
            NSApplication.Init ();

            using (var p = new NSAutoreleasePool ()) {
                NSApplication.SharedApplication.Delegate = new AppDelegate();
                NSApplication.Main(args);
            }
#elif __IOS__ || __TVOS__
            UIApplication.Main(args, null, "AppDelegate");
#else
RunGame();
#endif
}

#if __IOS__ || __TVOS__
        public override void FinishedLaunching(UIApplication app)
        {
            RunGame();
        }
#endif
}
#if MONOMAC
    class AppDelegate : NSApplicationDelegate
    {
        public override void FinishedLaunching (MonoMac.Foundation.NSObject notification)
        {
            AppDomain.CurrentDomain.AssemblyResolve += (object sender, ResolveEventArgs a) =>  {
                if (a.Name.StartsWith("MonoMac")) {
                    return typeof(MonoMac.AppKit.AppKitFramework).Assembly;
                }
                return null;
            };
            Program.RunGame();
        }

        public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
        {
            return true;
        }
    }  
#endif
}