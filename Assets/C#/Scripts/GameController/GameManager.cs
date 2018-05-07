using UnityEngine;
using System.Collections;
using Assets.Enums;
using UnityEngine.SceneManagement;
using System;
using Stages;

namespace Assets.Scripts.GameController
{
    public class GameManager
    {
        public static void StartGame()
        {
            Master.isPlaying = true;

            try
            {
                SceneManager.LoadScene(Master.Stage.SceneName, LoadSceneMode.Single);
            }
            catch(Exception ex)
            {
                Debug.Log("Could not load stage, resetting to Last Stand...");
                Master.Stage = Stage.GetStage(StageIds.LastStand);
                SceneManager.LoadScene("LastStand", LoadSceneMode.Single);
            }

            SceneManager.LoadScene("CharacterMaster", LoadSceneMode.Additive);
            //SceneManager.LoadScene("CharacterThumbnails", LoadSceneMode.Additive);
        }
    }
}