using Microsoft.Maui.Graphics.Text;
using Microsoft.UI.Text;

namespace MathGame;

public partial class GamePage : ContentPage
{
	public string GameType { get; set; }
	int firstNum = 0;
	int secondNum = 0;
	int score = 0;
	const int totalQuestions = 2; // TODO: Add ability to choose amount of questions
	int questionsLeft = totalQuestions;
	
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

    private void OnAnswerSubmit(object sender, EventArgs e)
    {
		if (!int.TryParse(AnswerEntry.Text, out int answer))
		{
			ResultLabel.Text = "Please enter a number";
			ResultLabel.TextColor = Colors.Red;
			return;
		}

		bool isCorrect = false;
		
		switch (GameType)
		{
			case "Addition":
				isCorrect = answer == firstNum + secondNum; 
				break;
			case "Subtraction":
				isCorrect = answer == firstNum - secondNum;
				break;
			case "Multiplication":
				isCorrect = answer == firstNum * secondNum;
				break;
			case "Division":
				isCorrect = answer == firstNum / secondNum;
				break;
		}

		if (isCorrect)
		{
            ResultLabel.Text = "Correct!";
            ResultLabel.TextColor = Colors.Green;
            score++;
        } else
		{
            ResultLabel.Text = "Wrong answer!";
            ResultLabel.TextColor = Colors.Red;
        }

		AnswerEntry.Text = "";

		if (--questionsLeft > 0) GenerateQuestion();
		else GameOver();
    }

	private void GameOver() {
        GameFieldStack.IsVisible = false;
		GameOverLabel.Text = $"Game Over! Your score: {score}/{totalQuestions}";
		BackToMenuBtn.IsVisible = true;
    }

    private void OnBackToMenu(object sender, EventArgs e)
    {
		Navigation.PushAsync(new MainPage());
    }
}