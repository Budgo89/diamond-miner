using System;
using Profile;
using UnityEngine;

namespace Tool
{
    public static class SaveManagement
    {
        /// <summary>
        /// Загрузить уровни
        /// </summary>
        /// <returns></returns>
        public static GameLevel GetLevels()
        {
            try
            {
                return new GameLevel(PlayerPrefs.GetInt("AvailableLevel"), PlayerPrefs.GetInt("CurrentLevel"));
            }
            catch (Exception e)
            {
                return new GameLevel();
            }
        }

        /// <summary>
        /// Сохранить уровни
        /// </summary>
        /// <param name="gameLevel"></param>
        public static void SetLevels(GameLevel gameLevel)
        {
            PlayerPrefs.SetInt("AvailableLevel", gameLevel.AvailableLevel);
            PlayerPrefs.SetInt("CurrentLevel", gameLevel.CurrentLevel);
        }

        /// <summary>
        /// Загрузить громкость
        /// </summary>
        /// <returns></returns>
        public static float GetVolume()
        {
            try
            {
                var a = PlayerPrefs.GetFloat("Volume");
                return PlayerPrefs.GetFloat("Volume");
            }
            catch (Exception e)
            {
                return 1f;
            }

            return 1f;
        }

        /// <summary>
        /// Сохранить громкость
        /// </summary>
        /// <param name="gameLevel"></param>
        public static void SetVolume(float volume)
        {
            PlayerPrefs.SetFloat("Volume", volume);
        }
        /// <summary>
        /// Перезагруска уровня
        /// </summary>
        /// <param name="x">0 новый запуск, 1 перезапуск</param>
        public static void SetRestart(int x)
        {
            PlayerPrefs.SetInt("Restart", x);
        }
        /// <summary>
        /// 0 новый запуск, 1 перезапуск
        /// </summary>
        /// <returns></returns>
        public static int GetRestart()
        {
            return PlayerPrefs.GetInt("Restart");
        }

        public static void SetGameState(int gameState)
        {
            PlayerPrefs.SetInt("GameState", gameState);
        }

        public static int GetGameState()
        {
            return PlayerPrefs.GetInt("GameState");
        }

        /// <summary>
        /// Перезагруска уровня
        /// </summary>
        /// <param name="x">0 обучение не пройдено, 1 обучение пройдено</param>
        public static void SetTraining(int x)
        {
            PlayerPrefs.SetInt("Training", x);
        }

        public static bool IsTraining()
        {
            var x = PlayerPrefs.GetInt("Training");
            return x == 0 ? false : true;
        }
    }
}
