using UnityEngine;
using System.Collections;
using System;
using Assets.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Server
{
    public class Game
    {
        private string charName;
        private Print print;
        public StageIds stageId;
        public CharacterIds characterId;

        public int pubGameId;
        public string privGameId;

        public bool isPrivate;
        public static List<GameListing> Listings(JSWrapper wrapper)
        {
            List<GameListing> retListings = new List<GameListing>();
            wrapper.On("gameListings", (data) => {
                GameListing[] gameListings = JsonConvert.DeserializeObject<GameListing[]>(data.ToString());
                retListings = gameListings.ToList();
            })
            .Emit("gameListings");
            return retListings;
        }
        public Game(JSWrapper wrapper, Print print, int pubId)
        {
            Init(wrapper, print);
            wrapper.Emit("requestPublic", pubId.ToString());
            pubGameId = pubId;
            isPrivate = false;
        }
        public Game(JSWrapper wrapper, Print print, string privateId)
        {
            Init(wrapper, print);
            wrapper.Emit("requestPrivate", privateId);
            privGameId = privateId;
            isPrivate = true;
        }
        public Game(JSWrapper wrapper, Print print)
        {
            Init(wrapper, print);
            wrapper.Emit("requestPrivate");
            isPrivate = true;
        }
        public void Init(JSWrapper wrapper, Print print)
        {
            this.wrapper = wrapper;
            this.print = print;
            wrapper
                .On("statics", (msg) => print(msg))
                .On("charecter", (msg) => print(msg));
        }
        public void SelectCharecter(string charName, Action<object> callback = null)
        {
            this.charName = charName;
            Debug.Log("setting charecter to " + charName);
            wrapper
                .On("start", callback)
                .Emit("requestCharecter", charName);
        }
        public void Move(double direction)//direction is measured in degreese
        {
            wrapper.Emit("move", direction);
        }
    }

}