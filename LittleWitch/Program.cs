using LittleWitch.Scenes;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;

string projectRoot = Directory.GetCurrentDirectory();
string projectName = Assembly.GetEntryAssembly().GetName().Name;
int pos = 0;
for (int i = 0; i < projectRoot.Length; i++)
{
    pos = projectRoot.IndexOf(projectName);
}
pos += projectName.Length;
projectRoot = projectRoot.Substring(0, pos);
Globals.ResourcesPath = Path.Combine(new string[] { projectRoot, "Resources" });
var game = new GameProcessor(
    new List<(string, string)>()
    {
        ("Scroll1", "Sprites/Scroll1")
    },
    new List<(string, string)>()
    {
                //("MainFont", "Fonts/SystemFont")
    }
    );
var mainMenuView = new MainMenuView();
var scnMainMenu = new Scene(mainMenuView, null, new Presenter(game, mainMenuView, null));

game.Scenes.Add("MainMenu", scnMainMenu);
game.SetCurrentScene("MainMenu");
game.Run();