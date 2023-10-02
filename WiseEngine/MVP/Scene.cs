namespace WiseEngine.MVP;
/// <summary>
/// Scene class which contains all scene MVP elements - View, Presenter and Model
/// </summary>
/// <remarks>
/// Scene must have <see cref="View"/>, but can not have <see cref="Model"/> and, thus, <see cref="Presenter"/>.
/// It might make sense for pure interface scenes such as menu. 
/// </remarks>
public class Scene
{
    /// <value>
    /// Property represents the <see cref="View"/>
    /// </value>
    public View View { get; set; }
    /// <value>
    /// Property represents the <see cref="Presenter"/>
    /// </value>
    public Presenter? Presenter { get; set; }
    /// <value>
    /// Property represents the <see cref="Model"/>
    /// </value>
    public Model? Model { get; set; }
    /// <value>
    /// Property represents flag which tells that the scene with all its parts (view, and model) was initialized
    /// </value>
    public bool IsInitalized { get; private set; }

    public Scene(View view, Model model, Presenter presenter)
    {
        View = view;
        Presenter = presenter;
        Model = model;
        IsInitalized = false;
    }
    /// <summary>
    /// Initialize all scene elements - view and model
    /// </summary>
    /// <remarks>
    /// Must be called before any work with scene
    /// </remarks>
    public void Initialize()
    {
        View.Initialize();
        if (Model != null)
        {
            Model.Initialize();
        }
        IsInitalized = true;
    }
    /// <summary>
    /// SolveCollision scene state 
    /// </summary>
    public void Update()
    {
        View.Update();
    }
    /// <summary>
    /// Draw scene 
    /// </summary>
    public void Draw()
    {
        View.Draw();
    }
}
