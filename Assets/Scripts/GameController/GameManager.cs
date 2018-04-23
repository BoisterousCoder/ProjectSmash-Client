using UnityEngine;
using System.Collections;
using Assets.Enums;
using UnityEngine.SceneManagement;
using Assets.Scripts.Server;
using Newtonsoft.Json;
using System;

namespace Assets.Scripts.GameController
{
    public class GameManager
    {
        public static void StartGame()
        {
            switch (Master.Game.stageId)
            {
                case (StageIds.LastStand):
                    SceneManager.LoadScene("LastStand", LoadSceneMode.Single);
                    break;
                default:
                    Debug.Log("Could not find stage \"" + Master.Game.stageId.ToString() + "\"");
                    break;
            }

            SceneManager.LoadScene("CharacterMaster", LoadSceneMode.Additive);
        }
    }
}