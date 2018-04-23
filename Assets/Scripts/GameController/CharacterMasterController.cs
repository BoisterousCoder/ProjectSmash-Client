using UnityEngine;
using System.Collections;
using Assets.Enums;
using System.Collections.Generic;
using System;
using Assets.Scripts;

public class CharacterMasterController : MonoBehaviour
{
    public static Dictionary<CharacterIds, GameObject> Characters = new Dictionary<CharacterIds, GameObject>();

    // Use this for initialization
    void Start()
    {
        GameObject GameObject;
        foreach (CharacterIds currentId in Enum.GetValues(typeof(CharacterIds)))
        {
            if(currentId != CharacterIds.Undefined)
            {
                GameObject = GameObject.Find(currentId.ToString());
                Characters.Add(currentId, GameObject);
                GameObject.SetActive(false);
            }
        }

        //FOR DEBUGGING
        Master.Game.characterId = CharacterIds.Taylor;
        CharacterMasterController.Characters[Master.Game.characterId].SetActive(true);
    }

    public static void SetAnimation(CharacterIds characterId, string animationName)
    {

    }
}
