namespace MathGame;

public partial class PreviousGamesPage : ContentPage
{
	public PreviousGamesPage()
	{
		InitializeComponent();
		gamesList.ItemsSource = App.GamesRepository.GetGames();
	}

    private void DeleteGame(object sender, EventArgs e)
    {
		Button btn = (Button)sender;
		App.GamesRepository.DeleteGame((int)btn.BindingContext);
        gamesList.ItemsSource = App.GamesRepository.GetGames();
    }
}