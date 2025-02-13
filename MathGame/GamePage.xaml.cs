using MathGame.Data;
using MathGame.Models;
using MathGame.WinUI;

namespace MathGame;

public partial class GamePage : ContentPage
{
	private Game _game { get; set; } = new Game();
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
		AssignGameType();
		BindingContext = this;
		GenerateQuestion();
	}

	private void AssignGameType()
	{
        _game.Type = GameType switch
		{
			"Addition" => GameOperation.Addition,
			"Subtraction" => GameOperation.Subtraction,
			"Multiplication" => GameOperation.Multiplication,
			"Division" => GameOperation.Division,
			_ => throw new ArgumentException("Invalid Operation")
		};
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
        _game.DatePlayed = DateTime.Now;
        _game.Score = score;
		
		App.GamesRepository.AddGame(_game); // Problem here, An object reference is required for the non-static field, method or property

        BackToMenuBtn.IsVisible = true;
    }

    private void OnBackToMenu(object sender, EventArgs e)
    {
		Navigation.PushAsync(new MainPage());
    }
}