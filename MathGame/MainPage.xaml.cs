namespace MathGame;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnGameChosen(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        Navigation.PushAsync(new GamePage(btn.AutomationId));
    }

    private void OnPreviosGamesChosen(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PreviousGamesPage());
    }
}