using UnityEngine;
using System.Collections;
using Assets.Enums;
using System;
using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;

namespace Characters
{
    public abstract class Character
    {
        #region Properties

        public abstract CharacterIds Id { get; protected internal set; }

        public abstract string Name { get; protected internal set; }

        public abstract string PlayerObjectName { get; protected internal set; }

        public abstract string ThumbnailName { get; protected internal set; }

        public GameObject PlayerObject
        {
            get
            {
                GameObject retObject = new GameObject();
                if (Master.isPlaying)
                {
                    retObject = GameObject.Find(PlayerObjectName);
                }
                return retObject;
            }

            protected internal set { }
        }

        public GameObject ThumbnailObject
        {
            get
            {
                GameObject retObject = GameObject.Find(ThumbnailName);
                if(retObject == null)
                {
                    retObject = new GameObject();
                }
                return retObject;
            }

            protected internal set { }
        }

        #endregion

        #region Methods

        public static Character GetCharacter(CharacterIds pId)
        {
            switch(pId)
            {
                case (CharacterIds.Taylor):
                    return new Taylor();
                case (CharacterIds.Erickson):
                    return new Erickson();
                case (CharacterIds.VonderEhe):
                    return new VonderEhe();
                case (CharacterIds.Shannon):
                    return new Shannon();
                default:
                    throw new Exception("Character not found. Id=" + pId);
            }
        }

        public static List<Character> GetAllCharacters()
        {
            List<Character> retList = new List<Character>();
            List<CharacterIds> idList = Enum.GetValues(typeof(CharacterIds)).Cast<CharacterIds>().ToList();

            foreach(CharacterIds currentId in idList)
            {
                Character newCharacter = GetCharacter(currentId);
                retList.Add(newCharacter);
            }

            return retList;
        }


        #endregion
    }
}
