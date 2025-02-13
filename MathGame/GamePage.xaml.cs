namespace MathGame;

public partial class GamePage : ContentPage
{
	public string GameType { get; set; }
	public GamePage(string gameType)
	{
		InitializeComponent();
		GameType = gameType;
		BindingContext = this;
	}
}