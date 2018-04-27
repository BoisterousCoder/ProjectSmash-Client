using UnityEngine;
using System.Collections;
using Assets.Enums;

namespace Characters
{
    public class Erickson : Character
    {
        private CharacterIds _Id = CharacterIds.Erickson;
        private string _Name = "Mr. Erickson";

        public override CharacterIds Id { 
            get
            {
                return _Id;
            }
            protected internal set { }
        }

        public override string Name
        {
            get
            {
                return _Name;
            }
            protected internal set { }
        }
    }
}