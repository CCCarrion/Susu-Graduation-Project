using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT
{
    //判断某个标识 是否为想要的值
    class CheckFlag: BT_Condition
    {
        public string tarFlag;

        public override bool Evaluate(Context context)
        {
            return context.flagSets.Contains(tarFlag);
        }
    }
}
