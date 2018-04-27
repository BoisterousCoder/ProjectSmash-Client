using UnityEngine;
using System.Collections;
using Assets.Enums;
using System.Collections.Generic;
using System;
using Assets.Scripts;
using Characters;

public class CharacterMasterController : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //FOR DEBUGGING
        //Master.Game.characterId = CharacterIds.Taylor;
        foreach (Character currentCharacter in Master.Characters)
        {
            currentCharacter.PlayerObject.SetActive(true);
        }
    }

    public static void SetAnimation(CharacterIds characterId, string animationName)
    {

    }
}
