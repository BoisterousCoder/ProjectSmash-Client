using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;
using System;
using Assets.Scripts.Server;
using System.Linq;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    enum serverConnectOptions
    {
        joinPublicGame = 0,
        privateGameInput = 1,
        privateGameJoin = 2,
        privateGameCreate = 3,
    }
    public class ServerConnect : MonoBehaviour
    {

        public List<GameObject> gameListingTitles = new List<GameObject>();
        public List<GameObject> gameListingPlayerCounts = new List<GameObject>();
        public List<GameObject> gameListingJoinButtons = new List<GameObject>();
        public List<GameListing> gameListings = new List<GameListing>();

        private GameObject selectJoinButton;
        private InputField selectInputPrivateGame;
        private GameObject selectJoinPrivateGame;
        private GameObject selectCreatePrivateGame;

        private int GameListingIndex = 0;

        private serverConnectOptions currentOption;

        // Use this for initialization
        void Start()
        {
            Master.Wrapper = new JSWrapper(Debug.Log, () => { });
            Master.Wrapper.openConnection();

            gameListingTitles = GameObject.FindGameObjectsWithTag("ServerConnectGameTitle").OrderBy(x => Regex.Match(x.name, @"\d+").Value).ToList();
            gameListingPlayerCounts = GameObject.FindGameObjectsWithTag("ServerConnectPlayerCount").OrderBy(x => Regex.Match(x.name, @"\d+").Value).ToList();
            gameListingJoinButtons = GameObject.FindGameObjectsWithTag("ServerConnectJoinButton").OrderBy(x => Regex.Match(x.name, @"\d+").Value).ToList();

            selectJoinButton = GameObject.Find("SelectJoinButton");
            selectInputPrivateGame = GameObject.Find("PrivateGameCodeInput").GetComponent<InputField>();
            selectJoinPrivateGame = GameObject.Find("SelectJoinPrivateGame");
            selectCreatePrivateGame = GameObject.Find("SelectCreatePrivateGame");

            gameListings = Game.Listings(Master.Wrapper);

            if(gameListings.Count > 0)
            {
                currentOption = serverConnectOptions.joinPublicGame;
            }
            else
            {
                currentOption = serverConnectOptions.privateGameInput;
            }
        }
       
        // Update is called once per frame
        void Update()
        {
            for(int i = 0; i < gameListingTitles.Count; i++)
            {
                if(gameListings.Count > i) // If there is a gameListing for this
                {
                    gameListingTitles[i].SetActive(true);
                    gameListingPlayerCounts[i].SetActive(true);
                    gameListingJoinButtons[i].SetActive(true);

                    gameListingPlayerCounts[i].GetComponent<Text>().text = gameListings[i].players + "/" + gameListings[i].maxPlayers;
                    if (gameListings[i].players == gameListings[i].maxPlayers)
                    {
                        gameListingPlayerCounts[i].GetComponent<Text>().color = new Color(40, 40, 200);
                        gameListingJoinButtons[i].SetActive(false);
                    }
                }
                else // If there isn't a gameListing for this
                {
                    gameListingTitles[i].SetActive(false);
                    gameListingPlayerCounts[i].SetActive(false);
                    gameListingJoinButtons[i].SetActive(false);
                }
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                switch (currentOption)
                {
                    case (serverConnectOptions.joinPublicGame):
                        {
                            if (GameListingIndex + 1 < gameListings.Count)
                            {
                                GameListingIndex++;
                            }
                        }
                        break;
                    case (serverConnectOptions.privateGameCreate):
                        currentOption = serverConnectOptions.privateGameInput;
                        break;
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                switch (currentOption)
                {
                    case (serverConnectOptions.joinPublicGame):
                        if (GameListingIndex - 1 >= 0)
                        {
                            GameListingIndex--;
                        }
                        break;
                    case (serverConnectOptions.privateGameInput):
                    case (serverConnectOptions.privateGameJoin):
                        currentOption = serverConnectOptions.privateGameCreate;
                        break;
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                switch (currentOption)
                {
                    case (serverConnectOptions.privateGameJoin):
                        currentOption = serverConnectOptions.privateGameInput;
                        break;
                    case (serverConnectOptions.privateGameCreate):
                    case (serverConnectOptions.privateGameInput):
                        if(gameListings.Count > 0)
                        {
                            currentOption = serverConnectOptions.joinPublicGame;
                        }
                        break;
                }
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                switch (currentOption)
                {
                    case (serverConnectOptions.joinPublicGame):
                        currentOption = serverConnectOptions.privateGameInput;
                        break;
                    case (serverConnectOptions.privateGameInput):
                        currentOption = serverConnectOptions.privateGameJoin;
                        break;
                }
            }

            if (Input.GetKey(KeyCode.Return))
            {
                JSWrapper wrapper = new JSWrapper(Debug.Log, () => { SceneManager.LoadScene("Character Select", LoadSceneMode.Single); });
                switch (currentOption)
                {
                    case (serverConnectOptions.joinPublicGame):
                        if(GameListingIndex < gameListings.Count && GameListingIndex >= 0)
                        {
                            if(gameListings[GameListingIndex].players < gameListings[GameListingIndex].maxPlayers)
                            {
                                string privateGameId = gameListings[GameListingIndex].id.ToString();
                                Master.Game = new Game(wrapper, Debug.Log, privateGameId);
                                
                            }
                        }
                        break;
                    case (serverConnectOptions.privateGameJoin):
                        Master.Game = new Game(wrapper, Debug.Log, selectInputPrivateGame.text);
                        break;
                    case (serverConnectOptions.privateGameInput):
                        currentOption = serverConnectOptions.privateGameJoin;
                        break;
                    case (serverConnectOptions.privateGameCreate):
                        Master.Game = new Game(wrapper, Debug.Log);
                        break;
                }
            }

            if (currentOption != serverConnectOptions.joinPublicGame && GameListingIndex != 0)
            {
                GameListingIndex = 0;
            }

            switch (currentOption)
            {
                case (serverConnectOptions.joinPublicGame):
                    if (GameListingIndex < gameListings.Count && GameListingIndex >= 0)
                    {
                        selectJoinButton.GetComponent<RectTransform>().position = gameListingJoinButtons[GameListingIndex].GetComponent<RectTransform>().position;
                        selectJoinButton.SetActive(true);
                    }
                    else
                    {
                        selectJoinButton.SetActive(false);
                    }
                    selectInputPrivateGame.DeactivateInputField();

                    selectJoinPrivateGame.SetActive(false);
                    selectCreatePrivateGame.SetActive(false);
                    break;
                case (serverConnectOptions.privateGameInput):
                    selectInputPrivateGame.Select();
                    selectInputPrivateGame.ActivateInputField();

                    selectJoinButton.SetActive(false);
                    selectJoinPrivateGame.SetActive(false);
                    selectCreatePrivateGame.SetActive(false);
                    break;
                case (serverConnectOptions.privateGameJoin):
                    selectInputPrivateGame.DeactivateInputField();

                    selectJoinButton.SetActive(false);
                    selectJoinPrivateGame.SetActive(true);
                    selectCreatePrivateGame.SetActive(false);
                    break;
                case (serverConnectOptions.privateGameCreate):
                    selectInputPrivateGame.DeactivateInputField();

                    selectJoinButton.SetActive(false);
                    selectJoinPrivateGame.SetActive(false);
                    selectCreatePrivateGame.SetActive(true);
                    break;
                
            }

        }
    }
}