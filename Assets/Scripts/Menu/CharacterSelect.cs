using Assets.Scripts.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class CharacterSelect : MonoBehaviour
    {
        public static Enums.CharacterIds[] characterSelectionList = new Enums.CharacterIds[4]
        {
            Enums.CharacterIds.Undefined,
            Enums.CharacterIds.Undefined,
            Enums.CharacterIds.Undefined,
            Enums.CharacterIds.Undefined
        };
        Canvas mainCanvas;
        Button taylorSelectButton;
        Button ericksonSelectButton;
        Button vondereheSelectButton;
        Button shannonSelectButton;

        Button goToStageSelectButton;

        Image player1;
        Image player2;
        Image player3;
        Image player4;

        Image player1Token;

        public static bool isPlayer1Selected = false;

        public void Start()
        {
            //string[] names = Input.GetJoystickNames();
            mainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            goToStageSelectButton = GameObject.Find("GoToStageSelect").GetComponent<Button>();
            

            //TODO: Make the player selects fit the screen
            player1 = GameObject.Find("Player1").GetComponent<Image>();
            player2 = GameObject.Find("Player2").GetComponent<Image>();
            player3 = GameObject.Find("Player3").GetComponent<Image>();
            player4 = GameObject.Find("Player4").GetComponent<Image>();

            player1Token = GameObject.Find("Player1Token").GetComponent<Image>();


            // Generate characters
            List<Image> characterSelectBoxes = GameObject.FindGameObjectsWithTag("CharacterSelectBox").Select(x => x.GetComponent<Image>()).OrderBy(x => Convert.ToInt32(x.name.Replace("CharacterBox", ""))).ToList();
            List<Text> characterSelectBoxTexts = GameObject.FindGameObjectsWithTag("CharacterSelectBoxText").Select(x => x.GetComponent<Text>()).OrderBy(x => Convert.ToInt32(x.name.Replace("CharacterBoxText", ""))).ToList();
            for (int i = 0; i < characterSelectBoxes.Count; i++)
            {
                //TODO: Get some kind of data collection for getting the players. We'll stick with a switch statement for now, but we need to change it

                switch ((Enums.CharacterIds)(i + 1))
                {
                    case (Enums.CharacterIds.Taylor):
                        characterSelectBoxTexts[i].text = "TAYLOR";
                        break;
                    case (Enums.CharacterIds.Erickson):
                        characterSelectBoxTexts[i].text = "ERICKSON";
                        break;
                    case (Enums.CharacterIds.VonderEhe):
                        characterSelectBoxTexts[i].text = "VONDEREHE";
                        break;
                    case (Enums.CharacterIds.Shannon):
                        characterSelectBoxTexts[i].text = "SHANNON";
                        break;
                }
                
            }

            // SET POSITIONS OF PLAYER SELECTS



            goToStageSelectButton.gameObject.SetActive(false);
            goToStageSelectButton.onClick.AddListener(GoToStageSelect);
        }

        public void Update()
        {
            if (isPlayer1Selected)
            {

            }
            
            // Detect controller

            // Show start button
            
            goToStageSelectButton.gameObject.SetActive(ArePlayersReady());
        }

        public void GoToStageSelect()
        {
            if(ArePlayersReady())
            {
                SceneManager.LoadScene("Stage Select", LoadSceneMode.Single);
            }
        }

        public bool ArePlayersReady()
        {
            foreach (Enums.CharacterIds currentCharId in characterSelectionList)
            {
                if (currentCharId != Enums.CharacterIds.Undefined)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
