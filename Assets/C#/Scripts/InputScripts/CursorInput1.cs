using UnityEngine;
using System.Collections;
using Assets.Scripts.Menu;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
using Characters;

namespace Assets.Scripts.InputScripts
{
    public class CursorInput1 : MonoBehaviour
    {

        public float cursorSpeed = 2;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 moveDir = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                moveDir.y += (float)1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveDir.x -= (float)1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveDir.y -= (float)1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveDir.x += (float)1;
            }
            transform.position += moveDir * cursorSpeed * Time.deltaTime;


            // For some reason, using just GetKeyDown makes it trigger multiple times, so I added a boolean to fix it.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                try
                {
                    if (CharacterSelect.isPlayer1Selected)
                    {
                        CharacterSelect.isPlayer1Selected = false;
                        return;
                    }
                    List<GameObject> characterSelectBoxes = GameObject.FindGameObjectsWithTag("CharacterSelectBox").ToList();
                    Transform tokenTransform = GameObject.Find("Player1Token").GetComponent<Transform>();
                    foreach (GameObject currentBox in characterSelectBoxes)
                    {
                        //TODO: Make it recognize token position
                        RectTransform currentBoxRectTransform = currentBox.GetComponent<RectTransform>();
                        if (currentBoxRectTransform.localPosition.x - (currentBoxRectTransform.rect.width / 2) < tokenTransform.localPosition.x && currentBoxRectTransform.localPosition.x + (currentBoxRectTransform.rect.width / 2) > tokenTransform.localPosition.x)
                        {
                            if (currentBoxRectTransform.localPosition.y - (currentBoxRectTransform.rect.height / 2) < tokenTransform.localPosition.y && currentBoxRectTransform.localPosition.y + (currentBoxRectTransform.rect.height / 2) > tokenTransform.localPosition.y)
                            {
                                if(Enum.IsDefined(typeof(Enums.CharacterIds), Convert.ToInt32(currentBox.name.Replace("CharacterBox", ""))))
                                {
                                    Master.Characters[0] = Character.GetCharacter((Enums.CharacterIds)Convert.ToInt32(currentBox.name.Replace("CharacterBox", "")));
                                    CharacterSelect.isPlayer1Selected = true;
                                    return;
                                }
                            }
                        }
                    }
                    CharacterSelect.isPlayer1Selected = false;
                    return;
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
