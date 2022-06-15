using System;
using System.IO;
using Profile;
using UnityEngine;

namespace Tool.Levels
{
    internal class LevelHandler
    {
        private const string Level = "LevelMenu.data";
        public GameLevel GetGameLevel()
        {
            var gameLevel = new GameLevel();
            try
            {
                using var filestreem = new StreamReader(Application.persistentDataPath + "//" + Level);
                while (!filestreem.EndOfStream)
                {
                    var text = filestreem.ReadLine();
                    gameLevel = new GameLevel(int.Parse(text));

                }
            }
            catch (Exception)
            {
                return new GameLevel();
            }
            return gameLevel;
        }

        public void SetGameLevel(GameLevel gameLevel)
        {
            using var filestreem = new StreamWriter(Application.persistentDataPath + "//" + Level);
            filestreem.WriteLine(gameLevel.AvailableLevel);
        }
    }
}
