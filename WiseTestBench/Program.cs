using System.Collections.Generic;
using System.IO;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseTestBench;
using WiseEngine;
using System.Reflection;
using WiseTestBench.BaseMovementScene;
using WiseTestBench.ButtonsWorkExampleScene;
using WiseTestBench.ExampleSceneTriggerWork;
using WiseTestBench.ExampleSceneShapeProjectileWork;

string projectRoot = Directory.GetCurrentDirectory();
string projectName = Assembly.GetEntryAssembly().GetName().Name;
int pos = 0;
for (int i = 0; i < projectRoot.Length; i++)
{
    pos = projectRoot.IndexOf(projectName);
}
pos += projectName.Length;
projectRoot = projectRoot.Substring(0, pos);
//string projectRoot = Path.GetFullPath(Path.Combine(new string []{currentDirectory}));
Globals.ResourcesPath = Path.Combine(new string []{projectRoot, "Resources"}); 
var game = new GameProcessor(
    new List<(string, string)>()
    {
                ("ExampleWitch", "ExampleWitch"),
                ("RedOrb", "RedOrb"),
                ("Goblin", "Goblin")
    },
    new List<(string, string)>()
    {
                ("MainFont", "DescriptionFont")
    }
    );

var v1 = new MainMenuView();
var scene1 = new Scene(v1, null, new Presenter(game, v1, null));
game.Scenes.Add("MainMenu", scene1);

var v2 = new BaseMovementView();
var m2 = new BaseMovementModel();
var scene2 = new Scene (v2, m2, new Presenter(game, v2, m2));

game.Scenes.Add("BaseMovement", scene2);

var v3 = new ButtonsWorkExampleView();
var m3 = new ButtonsWorkExampleModel();
var scene3 = new Scene (v3, m3, new Presenter (game, v3, m3));
game.Scenes.Add("BaseButtons", scene3);

var v4 = new TriggerWorkAndCameraExampleView();
var m4 = new TriggerWorkExampleModel();
var scene4 = new Scene(v4, m4, new Presenter(game, v4, m4));
game.Scenes.Add("Triggers", scene4);

var v5 = new ProjectileWorkView();
var m5 = new ProjectileWorkModel();
var scene5 = new Scene(v5, m5, new Presenter(game, v5, m5));
game.Scenes.Add("Projectiles", scene5);

var v6 = new ProjectileWorkView();
var m6 = new ProjectileWorkModel();
var scene6 = new Scene(v6, m6, new Presenter(game, v6, m6));
game.Scenes.Add("Physics", scene6);

game.SetCurrentScene("MainMenu");
game.Run();
