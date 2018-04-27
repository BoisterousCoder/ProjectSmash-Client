using UnityEngine;
using System.Collections;
using Assets.Enums;

namespace Characters
{
    public class Taylor : Character
    {
        private CharacterIds _Id = CharacterIds.Taylor;
        private string _Name = "Mr. Taylor";

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