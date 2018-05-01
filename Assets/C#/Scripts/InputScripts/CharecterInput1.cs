using Assets.Scripts.InputScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.C_.Scripts.InputScripts
{
    public class CharecterInput1 : CharacterInput
    {
        public new KeyCode leftKey = KeyCode.LeftArrow;
        public new KeyCode rightKey = KeyCode.RightArrow;
        public new KeyCode upKey = KeyCode.UpArrow;
        public new KeyCode attackKey = KeyCode.RightControl;
        public new KeyCode specialKey = KeyCode.RightAlt;
    }
}
