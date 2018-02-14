using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT
{
    class SetFlag : BT_Action
    {
        public string flag;

        public override void update(Context context, NodeContext nodeCtx)
        {
            base.update(context, nodeCtx);

            context.flagSets.Add(flag);
        }
    }
}
