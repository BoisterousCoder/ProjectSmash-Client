using Assets.Enums;
using Characters;
using Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Master
    {
        public static Stage Stage;
        public static Character[] Characters = new Character[4];

        public static bool isPlaying = false;
    }
}
