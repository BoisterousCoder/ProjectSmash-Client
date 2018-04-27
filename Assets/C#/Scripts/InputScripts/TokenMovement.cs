using UnityEngine;
using System.Collections;
using Assets.Scripts.Menu;
using UnityEngine.UI;

namespace Assets.Scripts.InputScripts
{
    public class TokenMovement : MonoBehaviour
    {
        Image token;
        Vector3 cursorPosition;
        // Use this for initialization
        void Start()
        {
            token = GameObject.Find("Player1Token").GetComponent<Image>();
            cursorPosition = GameObject.Find("Player1Glove").GetComponent<Transform>().position;
        }

        // Update is called once per frame
        void Update()
        {
            if (!CharacterSelect.isPlayer1Selected)
            {
                cursorPosition = GameObject.Find("Player1Glove").GetComponent<Transform>().position;
                cursorPosition.x -= (float).1;
                cursorPosition.y += (float).5;
                cursorPosition.z += 1;
                transform.position = cursorPosition;
            }
        }

        public static void MoveTokenToCursor()
        {

        }
    }
}
