using Assets.Enums;

namespace Stages
{
    public class LastStand : Stage
    {
        private StageIds _Id = StageIds.LastStand;
        private string _Name = "Last Stand";
        private string _SceneName = "LastStand";

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