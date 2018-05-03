using UnityEngine;
using System.Collections;
using Assets.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Stages
{
    public abstract class Stage
    {
        #region Properties

        public abstract StageIds Id { get; protected internal set; }

        public abstract string Name { get; protected internal set; }

        public abstract string SceneName { get; protected internal set; }

        #endregion

        #region Methods

        public static Stage GetStage(StageIds pId)
        {
            switch (pId)
            {
                case (StageIds.LastStand):
                    return new LastStand();
                case (StageIds.SchoolFront):
                    return new SchoolFront();
                default:
                    throw new Exception("Stage not found. Id=" + pId);
            }
        }

        public static List<Stage> GetAllStages()
        {
            List<Stage> retList = new List<Stage>();
            List<StageIds> idList = Enum.GetValues(typeof(StageIds)).Cast<StageIds>().ToList();

            foreach (StageIds currentId in idList)
            {
                Stage newCharacter = GetStage(currentId);
                retList.Add(newCharacter);
            }

            return retList;
        }

        #endregion
    }

}