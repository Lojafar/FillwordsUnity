using System;
using System.Collections.Generic;
namespace FillWords.Gameplay.Level
{
    [Serializable]
    public class LevelDataForJSON
    {
        public char[] LettersMap;
        public int LineSize;
        public string[] LevelWords;

        public LevelDataForJSON(LevelData levelData)
        {
            PrepareForSerialization(levelData);
        }
        void PrepareForSerialization(LevelData levelData)
        {
            LineSize = levelData.LettersField.GetLength(0);
            LettersMap = new char[LineSize * LineSize];
            for (int x = 0; x < LineSize; x++)
            {
                for (int y = 0; y < LineSize; y++)
                {
                    LettersMap[x + y * LineSize] = levelData.LettersField[x, y];
                }
            }
            LevelWords = new string[levelData.LevelWords.Count];
            int wordIndex = 0;
            foreach (string word in levelData.LevelWords)
            {
                LevelWords[wordIndex] = word;
                wordIndex++;
            }
        }
        public LevelData ToLevelData()
        {
            HashSet<string> WordsHashSet = new();
            foreach(string word in LevelWords)
            {
                WordsHashSet.Add(word);
            }
            char[,] LettersMap2D = new char[LineSize, LineSize];
            for(int i = 0; i < LettersMap.Length; i++)
            {
                int yIndex = i / LineSize;
                int xIndex = i - yIndex * LineSize;
                LettersMap2D[xIndex, yIndex] = LettersMap[i];
            }
            return new LevelData(LettersMap2D, WordsHashSet);
        }
    }
}
