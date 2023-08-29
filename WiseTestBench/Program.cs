using System.Collections.Generic;
using System.IO;
using System;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseTestBench;
using WiseEngine;

string currentDirectory = Directory.GetCurrentDirectory();
string projectRoot = Path.GetFullPath(Path.Combine(currentDirectory, "..\\..\\..\\"));
Globals.ResourcesPath = projectRoot+"\\Resources";
var game = new GameProcessor(
    new List<(string, string)>(),
    //{
    //            ("base_car","Base_car")
    //},
    new List<(string, string)>()
    {
                ("MainFont", "DescriptionFont")
    }
    );

var v1 = new MainMenuView();
var scene1 = new Scene(v1, null, new Presenter(game, v1, null));
game.Scenes.Add("Test1", scene1);

//var v2 = new SettingsMenuView();
//var scene2 = new Scene(v2, null, new Presenter(game, v2, null));
//game.Scenes.Add("Test2", scene2);

game.SetCurrentScene("Test1");
game.Run();
