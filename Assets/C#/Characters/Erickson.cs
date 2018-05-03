using UnityEngine;
using System.Collections;
using Assets.Enums;

namespace Characters
{
    public class Erickson : Character
    {
        private CharacterIds _Id = CharacterIds.Erickson;
        private string _Name = "Mr. Erickson";
        private string _PlayerObjectName = "Erickson";
        private string _ThumbnailName = "EricksonThumbnail";

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

        public override string PlayerObjectName
        {
            get
            {
                return _PlayerObjectName;
            }
            protected internal set { }
        }

        public override string ThumbnailName
        {
            get
            {
                return _ThumbnailName;
            }
            protected internal set { }
        }
    }
}