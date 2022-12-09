using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using Profile;
using UnityEngine;

namespace Tool
{
    public static class SaveManagement
    {

        public static string myPlayFabId;

        /// <summary>
        /// Загрузить уровни
        /// </summary>
        /// <returns></returns>
        public static GameLevel GetLevels()
        {
            var availableLevelLocal = PlayerPrefs.GetInt("AvailableLevel");
            var currentLevelLocal = PlayerPrefs.GetInt("CurrentLevel");

            var gameLevel = new GameLevel(availableLevelLocal, currentLevelLocal);
            try
            {
                if (PlayFabClientAPI.IsClientLoggedIn())
                {
                    PlayFabClientAPI.GetUserData(new GetUserDataRequest()
                    {
                        PlayFabId = myPlayFabId,
                        Keys = null
                    }, result =>
                    {
                        var availableLevelPlayFab = int.Parse(result.Data[gameLevel.AvailableLevelKey].Value);
                        var currentLevelLocalPlayFab = int.Parse(result.Data[gameLevel.CurrentLevelKey].Value);
                        
                        gameLevel.AvailableLevel = availableLevelLocal >= availableLevelPlayFab
                            ? availableLevelLocal
                            : availableLevelPlayFab;

                        gameLevel.CurrentLevel = currentLevelLocal >= currentLevelLocalPlayFab
                            ? currentLevelLocal
                            : currentLevelLocalPlayFab;
                        
                    }, (error) => {
                        Debug.Log("Got error retrieving user data:");
                        Debug.Log(error.GenerateErrorReport());
                    });

                    return gameLevel;
                }
                else
                    return gameLevel;
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
            if (PlayFabClientAPI.IsClientLoggedIn())
            {
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                    {
                        Data = new Dictionary<string, string>() {
                            {gameLevel.AvailableLevelKey, gameLevel.AvailableLevel.ToString()},
                            {gameLevel.CurrentLevelKey, gameLevel.CurrentLevel.ToString()}
                        }
                    },
                    result => Debug.Log("Successfully updated user data"),
                    error => {
                        Debug.Log("Got error setting user data Ancestor to Arthur");
                        Debug.Log(error.GenerateErrorReport());
                    });
            }


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

        /// <summary>
        /// Получить Количество побед в ПВП
        /// </summary>
        /// <returns></returns>
        public static int GetVictoryPvp()
        {
            int victoryPvp = 0;
            try
            {
                if (PlayFabClientAPI.IsClientLoggedIn())
                {
                    PlayFabClientAPI.GetUserData(new GetUserDataRequest()
                    {
                        PlayFabId = myPlayFabId,
                        Keys = null
                    }, result =>
                    {
                        victoryPvp = int.Parse(result.Data["VictoryPvp"].Value);

                    }, (error) => {
                        Debug.Log("Got error retrieving user data:");
                        Debug.Log(error.GenerateErrorReport());
                    });

                    return victoryPvp;
                }
                else
                    return victoryPvp;
            }
            catch (Exception e)
            {
                return victoryPvp;
            }
        }


        public static void SetVictoryPvp(int victoryPvp)
        {
            if (PlayFabClientAPI.IsClientLoggedIn())
            {
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                    {
                        Data = new Dictionary<string, string>() {
                            {"VictoryPvp", victoryPvp.ToString()}
                        }
                    },
                    result => Debug.Log("Successfully updated user data"),
                    error => {
                        Debug.Log("Got error setting user data Ancestor to Arthur");
                        Debug.Log(error.GenerateErrorReport());
                    });
            }
        }
    }
}
