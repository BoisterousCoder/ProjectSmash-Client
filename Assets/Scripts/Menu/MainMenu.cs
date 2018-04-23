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
    enum mainMenuOptions
    {
        smash = 0,
        options = 1
    }
    public class MainMenu : MonoBehaviour
    {
        private SpriteRenderer selectOptionsCircle;
        private SpriteRenderer selectSmashBox;

        private mainMenuOptions currentSelectedItem;
        

        public void Start()
        {
            selectOptionsCircle = GameObject.Find("SelectOptionsCircle").GetComponent<SpriteRenderer>();
            selectSmashBox = GameObject.Find("SelectSmashBox").GetComponent<SpriteRenderer>();

            selectOptionsCircle.enabled = false;
            selectSmashBox.enabled = false;
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(currentSelectedItem == mainMenuOptions.smash)
                {
                    currentSelectedItem = mainMenuOptions.options;
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentSelectedItem == mainMenuOptions.options)
                {
                    currentSelectedItem = mainMenuOptions.smash;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch (currentSelectedItem)
                {
                    case (mainMenuOptions.smash):
                        SceneManager.LoadScene("Main Menu Server Connect", LoadSceneMode.Single);
                        break;
                    case (mainMenuOptions.options):
                        SceneManager.LoadScene("Main Menu Options", LoadSceneMode.Single);
                        break;
                }
            }

            selectSmashBox.enabled = false;
            selectOptionsCircle.enabled = false;

            switch (currentSelectedItem)
            {
                case (mainMenuOptions.smash):
                    selectSmashBox.enabled = true;
                    break;
                case (mainMenuOptions.options):
                    selectOptionsCircle.enabled = true;
                    break;
            }
        }
    }
}
