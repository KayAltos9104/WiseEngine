using LittleWitch.Scenes;
using LittleWitch.Scenes.Levels;
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
        ("Platform1", "Sprites/Platform1"),
        ("Star", "Sprites/Star"),
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
        ("Witch_Run1", "Animations/Witch/Run/Runanimation1"),
        ("Witch_Run2", "Animations/Witch/Run/Runanimation2"),
        ("Witch_Run3", "Animations/Witch/Run/Runanimation3"),
        ("Witch_Run4", "Animations/Witch/Run/Runanimation4"),
        ("Witch_Run5", "Animations/Witch/Run/Runanimation5"),
        ("Witch_Run6", "Animations/Witch/Run/Runanimation6"),
        ("Witch_Run7", "Animations/Witch/Run/Runanimation7"),
        ("Witch_Run8", "Animations/Witch/Run/Runanimation8"),
        ("Witch_Run9", "Animations/Witch/Run/Runanimation9"),
        ("Witch_Run10", "Animations/Witch/Run/Runanimation10"),
        ("Witch_Run11", "Animations/Witch/Run/Runanimation11"),
        ("Witch_Run12", "Animations/Witch/Run/Runanimation12"),
        ("Witch_Run13", "Animations/Witch/Run/Runanimation13"),
        ("Witch_Run14", "Animations/Witch/Run/Runanimation14"),
        ("Witch_Run15", "Animations/Witch/Run/Runanimation15"),
        ("Witch_Run16", "Animations/Witch/Run/Runanimation16"),        
        ("Witch_Jump1", "Animations/Witch/Jump/Jumpanimation1"),
        ("Witch_Jump2", "Animations/Witch/Jump/Jumpanimation2"),
        ("Witch_Jump3", "Animations/Witch/Jump/Jumpanimation3"),
        ("Witch_Jump4", "Animations/Witch/Jump/Jumpanimation4"),
        ("Witch_Jump5", "Animations/Witch/Jump/Jumpanimation5"),
        ("Witch_Jump6", "Animations/Witch/Jump/Jumpanimation6"),
        ("Witch_Jump7", "Animations/Witch/Jump/Jumpanimation7"),
        ("Witch_Jump8", "Animations/Witch/Jump/Jumpanimation8"),
        ("Witch_Fall1", "Animations/Witch/Fall/Jumpanimation9"),
        ("Witch_Fall2", "Animations/Witch/Fall/Jumpanimation10"),
        ("Witch_Fall3", "Animations/Witch/Fall/Jumpanimation11"),
        ("Witch_Fall4", "Animations/Witch/Fall/Jumpanimation12"),
        ("Witch_Fall5", "Animations/Witch/Fall/Jumpanimation13"),
        ("Witch_Fall6", "Animations/Witch/Fall/Jumpanimation14"),
        ("Witch_Fall7", "Animations/Witch/Fall/Jumpanimation15"),
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
//game.SetCurrentScene("Level0");

game.Run();