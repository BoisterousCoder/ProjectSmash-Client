using UnityEngine;
using System.Collections;
using Assets.Enums;
using UnityEngine.SceneManagement;
using System;

namespace Assets.Scripts.GameController
{
    public class GameManager
    {
        public static void StartGame()
        {
            Master.isPlaying = true;
            switch (Master.StageId)
            {
                case (StageIds.LastStand):
                    SceneManager.LoadScene("LastStand", LoadSceneMode.Single);
                    break;
                default:
                    Debug.Log("Could not find stage \"" + Master.StageId.ToString() + "\"");
                    break;
            }

            SceneManager.LoadScene("CharacterMaster", LoadSceneMode.Additive);
        }
    }
}