using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WiseEngine.MVP;


namespace WiseEngine.MonogamePart;
/// <summary>
/// Main game class, which contains technical fields and manage game scenes
/// </summary>
public sealed class GameProcessor : Game
{
    //private GraphicsDeviceManager _graphics;
    private Scene? _currentScene;
    //private string? _pathToResources;
    private List<(string key, string path)> _textures;
    private List<(string key, string path)> _fonts;

    private TimeSpan _elapsedTime = new TimeSpan();
    private int _elapsedFrames = 0;
   
    /// <value>
    /// The <c>Scenes</c> property represents a dictionary with all scenes used in game
    /// </value>
    public Dictionary<string, Scene> Scenes { get; }

    

    /// <summary>
    /// This constructor initializes GameProcessor object with neccessary technical parameters 
    /// </summary>
    /// <param name="textures">Pairs of texture name in code and relative path to its file</param>
    /// <param name="fonts">Pairs of font name in code and relative path to its file</param>
    public GameProcessor(
        List<(string key, string path)> textures, 
        List<(string key, string path)> fonts)
    {
        Scenes = new Dictionary<string, Scene>();   
        Graphics2D.Graphics = new GraphicsDeviceManager(this);
        if (Globals.ResourcesPath == null)
        {
            throw new Exception("Resources path is missing");
        }
        Content.RootDirectory = Globals.ResourcesPath;        
        IsMouseVisible = true; 
        _textures = textures;
        _fonts = fonts;
    }
    /// <summary>
    /// Initialize game parameters 
    /// </summary>
    protected override void Initialize()
    {
        base.Initialize();
        Window.Title = "WiseEngine 0.2.2 alpha";        
        SetResolution(Globals.Resolution.Width, Globals.Resolution.Height);
        SetFullScreenMode(Globals.IsFullScreen);

        if (_currentScene == null)
        {
            throw new ArgumentNullException("Current scene was not choosed");
        }
        else
        {
            _currentScene.Initialize();
        }
    }
    /// <summary>
    /// Sets screen resolution 
    /// </summary>
    /// <param name="width">Screen width</param>
    /// <param name="height">Screen height</param>
    public void SetResolution (int width, int height)
    {
        if (width < 0 || height < 0)
            throw new ArgumentException ("Resolution cannot be negative!");
            
        Globals.Resolution = (width, height);
        Graphics2D.Graphics.PreferredBackBufferWidth = Globals.Resolution.Width;
        Graphics2D.Graphics.PreferredBackBufferHeight = Globals.Resolution.Height;           
        Graphics2D.Graphics.ApplyChanges();
        
    }
    /// <summary>
    /// Sets fullscreen mode true or false
    /// </summary>
    /// <param name="isFullScreen">True for fullscreen, false - not</param>
    public void SetFullScreenMode(bool isFullScreen)
    {
        Globals.IsFullScreen = isFullScreen;
        Graphics2D.Graphics.IsFullScreen = Globals.IsFullScreen;
        Graphics2D.Graphics.ApplyChanges();        
    }
    /// <summary>
    /// Loads graphics and game files
    /// </summary>    
    protected override void LoadContent()
    {
        Graphics2D.SpriteBatch = new SpriteBatch(GraphicsDevice);
        LoadableObjects.AddFont("SystemFont", Content.Load<SpriteFont>(Path.Combine(new string[]{Globals.ResourcesPath ,"SystemFont"})));

        foreach (var t in _textures)
        {
            LoadTexture(t.key, t.path);
        }
        foreach (var f in _fonts)
        {
            LoadFont(f.key, f.path);
        }
    }
    /// <summary>
    /// Loads texture from resources to game
    /// </summary>
    /// <param name="name">Texture name that will be used in game code</param>
    /// <param name="path">Relative path to the texture including its name without extension</param>
    public void LoadTexture (string name, string path)
    {
        LoadableObjects.AddTexture(name, Content.Load<Texture2D>(path));
    }
    /// <summary>
    /// Loads font from resources to game
    /// </summary>
    /// <param name="name">Font name that will be used in game code</param>
    /// <param name="path">Relative path to the font including its name without extension</param>
    public void LoadFont(string name, string path)
    {
        LoadableObjects.AddFont(name, Content.Load<SpriteFont>(path));
    }
    /// <summary>
    /// Updates scene
    /// </summary>
    /// <param name="gameTime"> GameTime element parameter </param>
    /// <remarks>
    /// Its override of original Monogame Update and is called automatically
    /// </remarks>
    protected override void Update(GameTime gameTime)
    {
        InputsManager.ReadInputs();
        if (_currentScene != null)
        {
            if (_currentScene.IsInitalized == false)
                _currentScene.Initialize();
            _currentScene.Update();
        }  
        Globals.Time = gameTime;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.LeftControl) && InputsManager.IsSinglePressed(Keys.Q))
            GameConsole.SwitchVisibility();
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.LeftControl) && InputsManager.IsSinglePressed(Keys.R))
            GameConsole.Clear();
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.LeftControl) && InputsManager.IsSinglePressed(Keys.S))
            Globals.SpriteBordersAreVisible = !Globals.SpriteBordersAreVisible;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.LeftControl) && InputsManager.IsSinglePressed(Keys.C))
            Globals.CollidersAreVisible = !Globals.CollidersAreVisible;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.LeftControl) && InputsManager.IsSinglePressed(Keys.F))
            Globals.FPSIsVisible = !Globals.FPSIsVisible;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.LeftControl) && InputsManager.IsSinglePressed(Keys.B))
            Globals.CameraAreaIsVisible = !Globals.CameraAreaIsVisible;

        //if (InputsManager.MouseStateCurrentFrame.LeftButton == ButtonState.Pressed)
        //{
        //    GameConsole.WriteLine($"Мышка: ({InputsManager.MouseStateCurrentFrame.X};{InputsManager.MouseStateCurrentFrame.Y})");
        //}

        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.LeftAlt) && InputsManager.IsSinglePressed(Keys.F4))
        {
            Exit();
        }
            

        InputsManager.SaveInputs();
        _elapsedTime += gameTime.ElapsedGameTime;
        _elapsedFrames++;
        if (_elapsedTime.TotalMilliseconds > 60000)
        {   
            _elapsedTime = new TimeSpan();
            _elapsedFrames = 0;
        }
           
        base.Update(gameTime);
    }
    /// <summary>
    /// Draw scene elements
    /// </summary>
    /// <param name="gameTime"> GameTime element parameter </param>
    /// <remarks>
    /// Its override of original Monogame Draw and is called automatically
    /// </remarks>
    protected override void Draw(GameTime gameTime)
    {        
        GraphicsDevice.Clear(Color.CornflowerBlue);

        
        if (_currentScene != null)
        {
            _currentScene.Draw();
        }
       

        Graphics2D.SpriteBatch.Begin();
        if (GameConsole.IsShown)    
            GameConsole.Render(Graphics2D.SpriteBatch);
        if (Globals.FPSIsVisible)
            Graphics2D.OutputText(Vector2.Zero, $"FPS: {(int)(_elapsedFrames / _elapsedTime.TotalSeconds)}");        

        Graphics2D.SpriteBatch.End();
        base.Draw(gameTime);
    }
    /// <summary>
    /// Sets scene which should be processed
    /// </summary>
    /// <param name="sceneName"> Scene name which is in scenes dictionary </param>
    public void SetCurrentScene(string sceneName)
    {
        if (Scenes.ContainsKey(sceneName))
            _currentScene = Scenes[sceneName];
        else
            GameConsole.WriteLine($"Scene {sceneName} is missed");
    }
    /// <summary>
    /// Initalize scene
    /// </summary>
    /// <param name="sceneName">Scene name to reload</param>
    public void ReloadScene (string sceneName)
    {
        if (Scenes.ContainsKey(sceneName))
            Scenes[sceneName].Initialize();
        else
            GameConsole.WriteLine($"Scene {sceneName} is missed");
    }
    /// <summary>
    /// Initializes current scene
    /// </summary>
    public void ReloadScene()
    {
        if (_currentScene != null)
            _currentScene.Initialize();
    }

}
