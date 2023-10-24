using LittleWitch.Scenes;
using LittleWitch.Scenes.Level0;
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
        ("Scroll1", "Sprites/Scroll1"),
        ("ForestBgrnd", "Sprites/Forest"),
        ("Witch_Idle1", "Animations/Witch/Idle/IdleAnimation1"),
        ("Witch_Idle2", "Animations/Witch/Idle/IdleAnimation2"),
        ("Witch_Idle3", "Animations/Witch/Idle/IdleAnimation3"),
        ("Witch_Idle4", "Animations/Witch/Idle/IdleAnimation4"),
        ("Witch_Idle5", "Animations/Witch/Idle/IdleAnimation5"),
        ("Witch_Idle6", "Animations/Witch/Idle/IdleAnimation6"),
        ("Witch_Idle7", "Animations/Witch/Idle/IdleAnimation7"),
        ("Witch_Idle8", "Animations/Witch/Idle/IdleAnimation8"),
        ("Witch_Idle9", "Animations/Witch/Idle/IdleAnimation9"),
        ("Witch_Idle10", "Animations/Witch/Idle/IdleAnimation10"),
        ("Witch_Idle11", "Animations/Witch/Idle/IdleAnimation11"),
        ("Witch_Idle12", "Animations/Witch/Idle/IdleAnimation12"),
        ("Witch_Idle13", "Animations/Witch/Idle/IdleAnimation13"),        
    },
    new List<(string, string)>()
    {
                ("MainFont", "Fonts/WitchFont")
    }
    );
var mainMenuView = new MainMenuView();
var scnMainMenu = new Scene(mainMenuView, null, new Presenter(game, mainMenuView, null));

var testLevelView = new CommonLevelView();
var testLevelModel = new ComonLevelModel();
var scnTestLevel = new Scene (testLevelView, testLevelModel, new Presenter (game, testLevelView, testLevelModel));

game.Scenes.Add("MainMenu", scnMainMenu);
game.Scenes.Add("Level0", scnTestLevel);
game.SetCurrentScene("MainMenu");
game.SetCurrentScene("Level0");

game.Run();