using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Menu;

namespace Assets.Scripts.InputScripts
{
    public class StageSelectPreviewSelected : MonoBehaviour
    {
        List<Sprite> stageSelectPreviews;
        GameObject currentStagePreview;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Master.Game.stageId != Enums.StageIds.Undefined && StageSelect.previewStages != null)
            {
                currentStagePreview = StageSelect.previewStages[(int)Master.Game.stageId - 1];
                Vector3 newVector = new Vector3();
                newVector.x = currentStagePreview.transform.position.x;
                newVector.y = currentStagePreview.transform.position.y;
                newVector.z = currentStagePreview.transform.position.z + (float).1;
                transform.position = newVector;
            }
        }
    }
}