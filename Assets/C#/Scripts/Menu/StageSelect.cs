using Assets.Scripts.GameController;
using Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class StageSelect : MonoBehaviour
    {
        List<Stage> allStages;
        Sprite selectedPreviewStage;
        public static List<GameObject> previewStages;

        Enums.StageIds selectedStage = Enums.StageIds.LastStand;
        Text selectedStageName;

        public void Start()
        {
            //string[] names = Input.GetJoystickNames();
            allStages = Stage.GetAllStages();
            previewStages = GameObject.FindGameObjectsWithTag("StageSelectPreview").OrderBy(x => Convert.ToInt32(x.name.Replace("StageSelectPreview", ""))).ToList();


            for (int i = allStages.Count; i < previewStages.Count; i++)
            {
                previewStages[i].SetActive(false);
                SpriteRenderer curSprite = previewStages[i].GetComponent<SpriteRenderer>();
                //curSprite.sprite
            }
            selectedPreviewStage = GameObject.Find("StageSelectPreviewSelected").GetComponent<Sprite>();

            selectedStageName = GameObject.Find("StageName").GetComponent<Text>();

            //previewStages[0].
        }

        public void Update()
        {
            #region Move selected 

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(allStages.Count > 4)
                {
                    int nextStageSelect = (int)selectedStage - (allStages.Count / 2);
                    if (nextStageSelect >= 1 && nextStageSelect <= allStages.Count)
                    {
                        selectedStage = (Enums.StageIds)nextStageSelect;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {

                if (allStages.Count > 4)
                {
                    int nextStageSelect = (int)selectedStage + (allStages.Count / 2);
                    if (nextStageSelect >= 1 && nextStageSelect <= allStages.Count)
                    {
                        selectedStage = (Enums.StageIds)nextStageSelect;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                int nextStageSelect = (int)selectedStage - 1;
                if (nextStageSelect >= 0 && nextStageSelect < allStages.Count)
                {
                    selectedStage = (Enums.StageIds)nextStageSelect;
                }
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                int nextStageSelect = (int)selectedStage + 1;
                if (nextStageSelect >= 0 && nextStageSelect < allStages.Count)
                {
                    selectedStage = (Enums.StageIds)nextStageSelect;
                }
            }

            #endregion

            Master.Stage = Stage.GetStage(selectedStage);

            selectedStageName.text = Master.Stage.Name;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                if (Master.Stage != null)
                {
                    GameManager.StartGame();
                }
            }

            //selectedPreviewStage. = previewStages[(int)selectedStage].rect.x;

            //if (selectedStage != Enums.StageIds.Undefined)
            //{
            //    startButton.gameObject.SetActive(true);
            //}
            //else
            //{
            //    startButton.gameObject.SetActive(false);
            //}
        }

        public void SelectStage(int stageId)
        {
            selectedStage = (Enums.StageIds)stageId;
        }
    }
}
