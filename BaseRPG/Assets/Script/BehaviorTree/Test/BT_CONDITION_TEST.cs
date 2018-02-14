using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT
{
    //判断某个标识 是否为想要的值
    class BT_CONDITION_TEST : BT_Condition
    {
        public bool bSwitch = true;
        public override bool Evaluate(Context context)
        {

            return bSwitch;
        }
    }
}
