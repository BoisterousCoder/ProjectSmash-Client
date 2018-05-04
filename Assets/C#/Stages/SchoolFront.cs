using Assets.Enums;

namespace Stages
{
    public class SchoolFront : Stage
    {
        private StageIds _Id = StageIds.SchoolFront;
        private string _Name = "School Front";
        private string _SceneName = "SchoolFront";

        public override StageIds Id
        {
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

        public override string SceneName
        {
            get
            {
                return _SceneName;
            }
            protected internal set { }
        }
    }
}