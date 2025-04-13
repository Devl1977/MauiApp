using System.Diagnostics;

using Microsoft.Maui.Controls;

using SammsGuessingGameFinal.GameLogic;

using System.Timers;

using System.Text.Json;

using System.IO;

using Plugin.Maui.Audio;

using MauiAudio = Plugin.Maui.Audio.AudioManager;



namespace SammsGuessingGameFinal;



public partial class MainPage : ContentPage

{

    private List<Card> _cards;

    private List<ImageButton> _cardButtons;

    private int _flippedCardsCount = 0;

    private ImageButton _firstFlippedCard;

    private ImageButton _secondFlippedCard;

    private System.Timers.Timer _countdownTimer;

    private int _remainingTime = 60;

    private int _matchedPairsCount = 0;

    private bool _isFlashing = false;
    private Color _originalBackgroundColor;
    private CancellationTokenSource _flashTokenSource;

    private bool _isGameInSession = false;

    private string _selectedDifficulty = null;





    private IAudioPlayer _matchSound;

    private IAudioPlayer _nomatchSound;

    private IAudioPlayer _winSound;

    private IAudioPlayer _timeoutSound;

    private IAudioPlayer _jeopardySound;



    public MainPage()

    {

        InitializeComponent();

        InitializeGame();

        LoadSounds();

        _originalBackgroundColor = CardGrid.BackgroundColor;

    }



    private async void LoadSounds()

    {

        try

        {

            _matchSound = MauiAudio.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Resources/Sounds/match.mp3"));

            _nomatchSound = MauiAudio.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Resources/Sounds/nomatch.mp3"));

            _winSound = MauiAudio.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Resources/Sounds/win.mp3"));

            _timeoutSound = MauiAudio.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Resources/Sounds/timeout.mp3"));

            _jeopardySound = MauiAudio.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Resources/Sounds/jeopardy.mp3"));

        }

        catch (Exception ex)

        {

            Debug.WriteLine($"❌ Error loading sounds: {ex.Message}");

        }

    }



    private void InitializeGame()

    {

        _cardButtons = new List<ImageButton>();

    }



    private void InitializeTimer()

    {

        if (_countdownTimer != null)

        {

            _countdownTimer.Stop();

            _countdownTimer.Dispose();

        }



        _countdownTimer = new System.Timers.Timer(1000);

        _countdownTimer.Elapsed += OnTimerTick;

        _countdownTimer.AutoReset = true;

    }



    private void OnTimerTick(object sender, ElapsedEventArgs e)

    {

        _remainingTime--;



        MainThread.BeginInvokeOnMainThread(() =>

        {

            TimerLabel.Text = $"Time Left: {_remainingTime} seconds";



            if (_remainingTime == 15)

            {

                _jeopardySound?.Play();

                if (!_isFlashing)
                {
                    _flashTokenSource = new CancellationTokenSource();
                    _ = FlashBackground(_flashTokenSource.Token);
                }

            }



            if (_remainingTime <= 0)

            {

                _countdownTimer.Stop();
                _jeopardySound?.Stop();
                _isGameInSession = false;

                _timeoutSound?.Play();

                if (_isFlashing && _flashTokenSource is not null)
                {
                    _flashTokenSource.Cancel();
                    _isFlashing = false;
                    CardGrid.BackgroundColor = _originalBackgroundColor;
                }

                DisplayAlert("Time's Up!", "Game over.", "OK");

            }

        });

    }



    private void StartButton_Click(object sender, EventArgs e)

    {

        if (_isGameInSession) return;



        if (string.IsNullOrEmpty(_selectedDifficulty))

        {

            DisplayAlert("Select Difficulty", "Please choose a difficulty level before starting!", "OK");

            return;

        }



        string playerName = PlayerNameEntry.Text?.Trim();

        if (string.IsNullOrEmpty(playerName))

        {

            DisplayAlert("Name Required", "Please enter your name before starting the game!", "OK");

            return;

        }



        _remainingTime = 60;

        InitializeTimer();

        _countdownTimer.Start();

        _isGameInSession = true;



        _cards = GenerateFakeCards(_selectedDifficulty);

        _matchedPairsCount = 0;

        _firstFlippedCard = null;

        _secondFlippedCard = null;



        CardGrid.Children.Clear();

        CardGrid.RowDefinitions.Clear();

        CardGrid.ColumnDefinitions.Clear();



        int rows = 4, columns = 4;

        switch (_selectedDifficulty)

        {

            case "Medium": rows = 5; columns = 6; break;

            case "Hard": rows = 9; columns = 12; break;

        }



        for (int i = 0; i < rows; i++)

            CardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });



        for (int i = 0; i < columns; i++)

            CardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });



        for (int i = 0; i < _cards.Count; i++)

        {

            var card = _cards[i];



            var imageButton = new ImageButton

            {

                Source = "cardback.jpg",

                BackgroundColor = Colors.Transparent,

                Aspect = Aspect.AspectFill,

                BindingContext = card.Value,

                HorizontalOptions = LayoutOptions.Center,

                VerticalOptions = LayoutOptions.Fill,

                MaximumWidthRequest = 100,

                MaximumHeightRequest = 120,

                Opacity = 1.0 // ✅ Prevent initial dimming 

            };



            imageButton.Clicked += CardButton_Click;



            Grid.SetRow(imageButton, i / columns);

            Grid.SetColumn(imageButton, i % columns);

            CardGrid.Children.Add(imageButton);

        }

    }



    private List<Card> GenerateFakeCards(string difficulty)

    {

        List<string> baseValues = difficulty switch

        {

            "Medium" => Enumerable.Range(1, 15).Select(i => $"flower{i}").ToList(),

            "Hard" => new List<string> {

                "twoofclubs", "threeofclubs", "fourofclubs", "fiveofclubs", "sixofclubs", "sevenofclubs", "eightofclubs", "nineofclubs", "tenofclubs",
                "jofclubs", "qofclubs", "kofclubs", "aofclubs",

                "twoofdiamonds", "threeofdiamonds", "fourofdiamonds", "fiveofdiamonds", "sixofdiamonds", "sevenofdiamonds", "eightofdiamonds", "nineofdiamonds", "tenofdiamonds",
                "jofdiamonds", "qofdiamonds", "kofdiamonds", "aofdiamonds",

                "twoofhearts", "threeofhearts", "fourofhearts", "fiveofhearts", "sixofhearts", "sevenofhearts", "eightofhearts", "nineofhearts", "tenofhearts",
                "jofhearts", "qofhearts", "kofhearts", "aofhearts",
    
                "twoofspades", "threeofspades", "fourofspades", "fiveofspades", "sixofspades", "sevenofspades", "eightofspades", "nineofspades", "tenofspades",
                "jofspades", "qofspades", "kofspades", "aofspades",

                "redjoker", "blackjoker"

            },

            _ => new List<string> { "aquacar", "redcar", "yellowcar", "bluecar", "purplecar", "greencar", "orangecar", "warpcar" }

        };



        return baseValues.Concat(baseValues).OrderBy(x => Guid.NewGuid()).Select(val => new Card { Value = val }).ToList();

    }



    private async void CardButton_Click(object sender, EventArgs e)

    {

        if (sender is not ImageButton imageButton || imageButton.BindingContext is null)

            return;



        string cardValue = imageButton.BindingContext.ToString();

        imageButton.Source = $"{cardValue}.jpg";

        imageButton.IsEnabled = false;

        imageButton.Opacity = 1.0; // ✅ Ensure full visibility on flip 



        if (_firstFlippedCard == null)

        {

            _firstFlippedCard = imageButton;

        }

        else

        {

            _secondFlippedCard = imageButton;



            foreach (var btn in CardGrid.Children.OfType<ImageButton>())

                btn.IsEnabled = false;



            await Task.Delay(500);



            string firstVal = _firstFlippedCard.BindingContext.ToString();

            string secondVal = _secondFlippedCard.BindingContext.ToString();



            if (firstVal == secondVal)

            {

                _matchedPairsCount++;

                _matchSound?.Play();



                if (_matchedPairsCount == _cards.Count / 2)

                {

                    _countdownTimer.Stop();

                    _isGameInSession = false;

                    _winSound?.Play();



                    var score = new PlayerScore(PlayerNameEntry.Text?.Trim(), _remainingTime);

                    SaveScore(score);

                    _jeopardySound?.Stop();
                    _flashTokenSource?.Cancel();
                    _isFlashing = false;
                    CardGrid.BackgroundColor = _originalBackgroundColor;

                    await DisplayAlert("You Win!", $"Time left: {_remainingTime} seconds", "Nice!");

                }

            }

            else

            {

                _nomatchSound?.Play();

                _firstFlippedCard.Source = "cardback.jpg";

                _secondFlippedCard.Source = "cardback.jpg";

                _firstFlippedCard.IsEnabled = true;

                _secondFlippedCard.IsEnabled = true;

                _firstFlippedCard.Opacity = 1.0; // ✅ Reset opacity 

                _secondFlippedCard.Opacity = 1.0; // ✅ Reset opacity 

            }



            _firstFlippedCard = null;

            _secondFlippedCard = null;



            foreach (var btn in CardGrid.Children.OfType<ImageButton>())

                btn.IsEnabled = true;

        }

    }



    private void SaveScore(PlayerScore score)

    {

        string filePath = Path.Combine(FileSystem.AppDataDirectory, "highscores.json");

        var scores = File.Exists(filePath)

            ? JsonSerializer.Deserialize<List<PlayerScore>>(File.ReadAllText(filePath)) ?? new()

            : new List<PlayerScore>();



        scores.Add(score);

        scores = scores.OrderByDescending(s => s.TimeLeft).Take(5).ToList();

        File.WriteAllText(filePath, JsonSerializer.Serialize(scores, new JsonSerializerOptions { WriteIndented = true }));

    }



    private void ViewHighScoresButton_Click(object sender, EventArgs e)

    {

        string path = Path.Combine(FileSystem.AppDataDirectory, "highscores.json");



        if (File.Exists(path))

        {

            var scores = JsonSerializer.Deserialize<List<PlayerScore>>(File.ReadAllText(path)) ?? new();

            string text = "🏆 High Scores:\n\n" + string.Join("\n", scores.Select(s => $"{s.PlayerName} – {s.TimeLeft} seconds"));

            DisplayAlert("Top 5 High Scores", text, "Close");

        }

        else

        {

            DisplayAlert("High Scores", "No high scores yet – be the first!", "OK");

        }

    }



    private async void ResetHighScoresButton_Click(object sender, EventArgs e)

    {

        bool confirm = await DisplayAlert("Confirm Reset", "Are you sure you want to delete all high scores?", "Yes", "Cancel");



        string path = Path.Combine(FileSystem.AppDataDirectory, "highscores.json");

        if (confirm)

        {

            if (File.Exists(path))

            {

                File.Delete(path);

                await DisplayAlert("High Scores Reset", "All high scores have been cleared.", "OK");

            }

            else

            {

                await DisplayAlert("No Scores", "There are no scores to clear yet!", "OK");

            }

        }

    }



    private void OnDifficultySelected(object sender, EventArgs e)

    {

        if (sender is Button button)

        {

            _selectedDifficulty = button.Text;

            foreach (var btn in ((HorizontalStackLayout)button.Parent).Children.OfType<Button>())

                btn.BackgroundColor = btn == button ? Colors.LightBlue : Colors.LightGray;

        }

    }

    private async Task FlashBackground(CancellationToken token)
    {
        _isFlashing = true;
        bool toggle = false;

        while (!token.IsCancellationRequested)
        {
            var newColor = toggle ? Colors.LightPink : Colors.White;
            CardGrid.BackgroundColor = newColor;
            toggle = !toggle;

            await Task.Delay(500, token);  // change to 300ms for faster flashing
        }

        CardGrid.BackgroundColor = _originalBackgroundColor;
        _isFlashing = false;
    }

}