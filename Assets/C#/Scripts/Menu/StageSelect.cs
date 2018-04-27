using Assets.Scripts.GameController;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class StageSelect : MonoBehaviour
    {

        public static List<GameObject> previewStages;
        Sprite selectedPreviewStage;

        Enums.StageIds selectedStage = Enums.StageIds.Stage1;
        Text selectedStageName;

        public void Start()
        {
            //string[] names = Input.GetJoystickNames();

            previewStages = GameObject.FindGameObjectsWithTag("StageSelectPreview").OrderBy(x => Convert.ToInt32(x.name.Replace("StageSelectPreview", ""))).ToList();
            selectedPreviewStage = GameObject.Find("StageSelectPreviewSelected").GetComponent<Sprite>();

            selectedStageName = GameObject.Find("StageName").GetComponent<Text>();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                int nextStageSelect = (int)selectedStage - (previewStages.Count / 2);
                if (nextStageSelect >= 1 && nextStageSelect <= previewStages.Count)
                {
                    selectedStage = (Enums.StageIds)nextStageSelect;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                int nextStageSelect = (int)selectedStage + (previewStages.Count / 2);
                if (nextStageSelect >= 1 && nextStageSelect <= previewStages.Count)
                {
                    selectedStage = (Enums.StageIds)nextStageSelect;
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if ((int)selectedStage != 1 && (int)selectedStage != previewStages.Count / 2 + 1)
                {
                    int nextStageSelect = (int)selectedStage - 1;
                    if (nextStageSelect >= 1 && nextStageSelect <= previewStages.Count)
                    {
                        selectedStage = (Enums.StageIds)nextStageSelect;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if((int)selectedStage != previewStages.Count / 2 && (int)selectedStage != previewStages.Count)
                {
                    int nextStageSelect = (int)selectedStage + 1;
                    if (nextStageSelect >= 1 && nextStageSelect <= previewStages.Count)
                    {
                        selectedStage = (Enums.StageIds)nextStageSelect;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (selectedStage != Enums.StageIds.Undefined)
                {
                    Master.StageId = selectedStage;
                    GameManager.StartGame();
                }
            }

            Master.StageId = selectedStage;
            selectedStageName.text = selectedStage.ToString();

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
