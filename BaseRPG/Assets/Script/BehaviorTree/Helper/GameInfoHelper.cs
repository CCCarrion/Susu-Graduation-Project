using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BT.Helper
{
    public class GameInfoHelper
    {

        //获取游戏世界运行时间
        static public float GetGameRunTime()
        {
            return Time.time;
        }

        //获取上一帧的时间差
        static public float GetDeltaTimeFromLastFrame()
        {
            return Time.deltaTime;
        }
    }
}
