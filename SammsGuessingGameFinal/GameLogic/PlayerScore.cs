namespace SammsGuessingGameFinal.GameLogic
{
    public class PlayerScore
    {
        public string PlayerName { get; set; }
        public int TimeLeft { get; set; }

        public PlayerScore(string playerName, int timeLeft)
        {
            PlayerName = playerName;
            TimeLeft = timeLeft;
        }
    }
}
