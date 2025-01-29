using System.Collections.Generic;
namespace FillWords.Gameplay.Level
{
    public class LevelData
    {
        public readonly char[,] LettersField;
        public readonly HashSet<string> LevelWords;

        public LevelData(char[,] lettersField, HashSet<string> levelWords)
        {
            LettersField = lettersField;
            LevelWords = levelWords;
        }
    }
}
