using UnityEngine;
using System.Collections;
using Assets.Enums;

namespace Characters
{
    public class VonderEhe : Character
    {
        private CharacterIds _Id = CharacterIds.VonderEhe;
        private string _Name = "Mr. Von der Ehe";
        private string _PlayerObjectName = "VonderEhe";
        private string _ThumbnailName = "VonderEheThumbnail";

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