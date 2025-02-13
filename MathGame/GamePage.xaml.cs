namespace MathGame;

public partial class GamePage : ContentPage
{
	public string GameType { get; set; }
	int firstNum = 0;
	int secondNum = 0;

	public GamePage(string gameType)
	{
		InitializeComponent();
		GameType = gameType;
		BindingContext = this;
		GenerateQuestion();
	}

	private void GenerateQuestion()
	{
		var operand = GameType switch
		{
			"Addition" => '+',
			"Subtraction" => '-',
			"Multiplication" => '*',
			"Division" => '/',
			_ => throw new ArgumentException("Invalid Operation!")
		};

		var random = new Random();

        firstNum = GameType != "Division" ? random.Next(1, 10) : random.Next(1, 101);
        secondNum = GameType != "Division" ? random.Next(1, 10) : random.Next(1, 101);

		if (GameType == "Division")
		{
			while (firstNum < secondNum || firstNum % secondNum != 0)
			{
				firstNum = random.Next(1, 101);
				secondNum = random.Next(1, 101);
			}
		}

		QuestionLabel.Text = $"{firstNum} {operand} {secondNum} ?";
    }
}