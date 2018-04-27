using Assets.Enums;
using Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Master
    {
        public static StageIds StageId = StageIds.Undefined;
        public static Character[] Characters = new Character[4];

        public static bool isPlaying = false;
    }
}
