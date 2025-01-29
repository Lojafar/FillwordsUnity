namespace FillWords.Gameplay.Level
{
    class DummyLevelLoader : ILevelLoader
    {
        LevelData currentLevel;
        int currentLevelId = 0;
        public LevelData LoadLevel(int levelNum)
        {
            if (currentLevel == null || currentLevelId != levelNum) 
            {
                currentLevel = LoadDummyLevel(levelNum);
                currentLevelId = levelNum;
            }
            return currentLevel;
        }
        LevelData LoadDummyLevel(int levelNum) 
        {
            if (levelNum % 2 == 0)
            {
                System.Collections.Generic.HashSet<string> words = new();
                words.Add("MIX");
                words.Add("COW");
                words.Add("WOW");
                var letters = new char[,] {
                { 'M', 'O', 'W' },
                { 'I', 'W', 'W'},
                { 'X', 'C', 'O'},
            };
                return new LevelData(letters, words);
            }
            else
            {
                System.Collections.Generic.HashSet<string> words = new();
                words.Add("APPLE");
                words.Add("JUICE");
                words.Add("LAPTOP");
                var letters = new char[,] {
                { 'A', 'P', 'P' , 'L'},
                { 'J', 'L', 'A', 'E'},
                { 'U', 'E', 'P', 'P'},
                { 'I', 'C', 'T', 'O'}
            };
                return new LevelData(letters, words);
            }
        }
    }
}