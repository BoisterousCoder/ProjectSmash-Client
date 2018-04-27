using UnityEngine;
using System.Collections;
using Assets.Enums;
using System;
using Assets.Scripts;

namespace Characters
{
    public abstract class Character
    {
        #region Properties
        public abstract CharacterIds Id { get; protected internal set; }

        public abstract string Name { get; protected internal set; }
        
        public GameObject PlayerObject
        {
            get
            {
                GameObject retObject = new GameObject();
                if (Master.isPlaying)
                {
                    retObject = GameObject.Find(Id.ToString());
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

        #endregion
    }
}
