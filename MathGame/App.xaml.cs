using MathGame.Data;

namespace MathGame;

public partial class App : Application
{
    public GamesRepository GamesRepository { get; set; }
    public App(GamesRepository gamesRepository)
    {
        InitializeComponent();
        GamesRepository = gamesRepository;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}