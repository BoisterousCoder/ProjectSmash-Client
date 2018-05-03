using UnityEngine;
using System.Collections;
using Assets.Enums;

namespace Characters
{
    public class Taylor : Character
    {
        private CharacterIds _Id = CharacterIds.Taylor;
        private string _Name = "Mr. Taylor";
        private string _PlayerObjectName = "Taylor";
        private string _ThumbnailName = "TaylorThumbnail";

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