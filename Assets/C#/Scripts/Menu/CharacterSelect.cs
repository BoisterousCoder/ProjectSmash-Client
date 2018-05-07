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
        List<Image> characterSelectBoxTitles;
        List<Text> characterSelectBoxTexts;
        List<Image> playerCards;
        List<Character> allCharacterList;

        Dictionary<string, GameObject> smallBoxThumbnails = new Dictionary<string, GameObject>();
        Dictionary<string, GameObject> largeBoxThumbnails = new Dictionary<string, GameObject>();

        Button goToStageSelectButton;

        private bool isSmallThumbnailSceneLoaded = false;
        private bool isBigThumbnailSceneLoaded = false;
        public static bool isPlayer1Selected = false;
        public static bool isPlayer2Selected = false;

        public void Start()
        {
            
            goToStageSelectButton = GameObject.Find("GoToStageSelect").GetComponent<Button>();

            SceneManager.LoadScene("CharacterThumbnails", LoadSceneMode.Additive);
            SceneManager.LoadScene("CharacterThumbnails", LoadSceneMode.Additive);

            // Generate characters
            characterSelectBoxes = GameObject.FindGameObjectsWithTag("CharacterSelectBox").Select(x => x.GetComponent<Image>()).OrderBy(x => Convert.ToInt32(x.name.Replace("CharacterBox", ""))).ToList();
            characterSelectBoxTitles = GameObject.FindGameObjectsWithTag("CharacterSelectBoxTitle").Select(x => x.GetComponent<Image>()).OrderBy(x => Convert.ToInt32(x.name.Replace("CharacterBoxTitle", ""))).ToList();
            characterSelectBoxTexts = GameObject.FindGameObjectsWithTag("CharacterSelectBoxText").Select(x => x.GetComponent<Text>()).OrderBy(x => Convert.ToInt32(x.name.Replace("CharacterBoxText", ""))).ToList();

            allCharacterList = Character.GetAllCharacters();

            for(int i = 0; i < characterSelectBoxes.Count; i++)
            {
                if (i >= allCharacterList.Count)
                {
                    characterSelectBoxes[i].gameObject.SetActive(false);
                    characterSelectBoxTitles[i].gameObject.SetActive(false);
                    characterSelectBoxTexts[i].gameObject.SetActive(false);
                }
                else
                {
                    characterSelectBoxTexts[i].text = allCharacterList[i].Name.ToUpper();
                }
                //characterList
            }
           
            goToStageSelectButton.gameObject.SetActive(false);
            goToStageSelectButton.onClick.AddListener(GoToStageSelect);
        }

        public void Update()
        {
            #region Initialize Thumbnails

            if (!isSmallThumbnailSceneLoaded) // Check if these are loaded here because it takes a while for Unity to load the scenes
            {
                foreach (Character currentChar in allCharacterList)
                {
                    List<GameObject> allCharThumbnails = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == currentChar.ThumbnailName).ToList();
                    if (allCharThumbnails.Count > 1)
                    {
                        isBigThumbnailSceneLoaded = true;
                        isSmallThumbnailSceneLoaded = true;
                        break;
                    }
                    if (allCharThumbnails.Count > 0)
                    {
                        isSmallThumbnailSceneLoaded = true;
                        break;
                    }
                }
                if (isBigThumbnailSceneLoaded)
                {
                    foreach (Character currentChar in allCharacterList)
                    {
                        List<GameObject> allCharThumbnails = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == currentChar.ThumbnailName).ToList();
                        if (allCharThumbnails.Count > 1)
                        {
                            smallBoxThumbnails.Add(currentChar.ThumbnailName, allCharThumbnails[0]);
                            largeBoxThumbnails.Add(currentChar.ThumbnailName, allCharThumbnails[1]);
                        }
                    }
                    foreach (GameObject curBigThumbnail in largeBoxThumbnails.Values)
                        curBigThumbnail.SetActive(false);
                }
                if (isSmallThumbnailSceneLoaded)
                {
                    for (int i = 0; i < allCharacterList.Count; i++) // Keep this here because scenes don't load immediately
                    {
                        Vector3 newVector = characterSelectBoxes[i].transform.position;
                        newVector.z = 89.5f;
                        smallBoxThumbnails[allCharacterList[i].ThumbnailName].transform.position = newVector;

                        float size = 0.4f;
                        //characterList[i].ThumbnailObject.transform.localScale = characterSelectBoxes[i].transform.localScale;
                        smallBoxThumbnails[allCharacterList[i].ThumbnailName].transform.localScale = new Vector3(size, size, size);
                    }
                }
            }

            #endregion


            foreach (Character currentChar in Master.Characters)
            {
                if(currentChar != null)
                {

                    //largeBoxThumbnails[currentChar.ThumbnailName].position = ;
                }
            }
            
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
