using Characters;
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
        List<Image> characterSelectBoxes;
        List<Text> characterSelectBoxTexts;
        List<Character> characterList;

        Button goToStageSelectButton;

        public static bool isPlayer1Selected = false;
        public static bool isPlayer2Selected = false;

        public void Start()
        {
            
            goToStageSelectButton = GameObject.Find("GoToStageSelect").GetComponent<Button>();

            SceneManager.LoadScene("CharacterThumbnails", LoadSceneMode.Additive);

            // Generate characters
            characterSelectBoxes = GameObject.FindGameObjectsWithTag("CharacterSelectBox").Select(x => x.GetComponent<Image>()).OrderBy(x => Convert.ToInt32(x.name.Replace("CharacterBox", ""))).ToList();
            characterSelectBoxTexts = GameObject.FindGameObjectsWithTag("CharacterSelectBoxText").Select(x => x.GetComponent<Text>()).OrderBy(x => Convert.ToInt32(x.name.Replace("CharacterBoxText", ""))).ToList();

            characterList = Character.GetAllCharacters();

            for(int i = 0; i < characterSelectBoxes.Count; i++)
            {
                if (i >= characterList.Count)
                {
                    characterSelectBoxes[i].gameObject.SetActive(false);
                    characterSelectBoxTexts[i].gameObject.SetActive(false);
                }
                else
                {
                    characterSelectBoxTexts[i].text = characterList[i].Name.ToUpper();
                }
                //characterList
            }
            
           
            goToStageSelectButton.gameObject.SetActive(false);
            goToStageSelectButton.onClick.AddListener(GoToStageSelect);
        }

        public void Update()
        {
            Debug.Log(GameObject.Find(characterList[0].ThumbnailName));
            for (int i = 0; i < characterList.Count; i++)
            {
                Vector3 newVector = characterSelectBoxes[i].transform.position;
                newVector.z = characterList[i].ThumbnailObject.transform.position.z;
                characterList[i].ThumbnailObject.transform.position = newVector;

                float test = 0.4f;
                //characterList[i].ThumbnailObject.transform.localScale = characterSelectBoxes[i].transform.localScale;
                characterList[i].ThumbnailObject.transform.localScale = new Vector3(test, test, test);
            }

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
            foreach (Character currentCharacter in Master.Characters)
            {
                if (currentCharacter != null)
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
