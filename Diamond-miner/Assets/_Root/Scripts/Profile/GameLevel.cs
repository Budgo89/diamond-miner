namespace Profile
{
    public class GameLevel
    {
        public int AvailableLevel = 0;
        public int CurrentLevel = 0;
        public int IsSameLevel = 0;

        public GameLevel()
        {
        }

        public GameLevel(int availableLevel)
        {
            AvailableLevel = availableLevel;
            CurrentLevel = availableLevel;
        }
    }
}
