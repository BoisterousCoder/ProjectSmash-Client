using System.Collections;
using Assets.Enums;
using UnityEngine;
using Assets.Scripts;

namespace Characters
{
    public class Shannon : Character
    {
        private CharacterIds _Id = CharacterIds.Shannon;
        private string _Name = "Mrs. Shannon";

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