using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT
{
    //Wait for some seconds
    public class BT_WaitForSeconds : BT_Action
    {
        public class BT_WaitForSecondsContext : NodeContext
        {
            public float spendSeconds;
        }

        public float totalSeconds;

        public override NodeContext CreateNodeContext(Context context)
        {
            return new BT_WaitForSecondsContext();
        }

        public override void start(Context context, NodeContext nodeCtx)
        {
            base.start(context, nodeCtx);
            (nodeCtx as BT_WaitForSecondsContext).spendSeconds = 0;
        }

        public override void update(Context context, NodeContext nodeCtx)
        {
            base.update(context, nodeCtx);

            BT_WaitForSecondsContext nodectx = nodeCtx as BT_WaitForSecondsContext;

            nodectx.spendSeconds += Helper.GameInfoHelper.GetDeltaTimeFromLastFrame();
            if (nodectx.spendSeconds >= totalSeconds)
            {
                nodectx.CurNodeState = NODE_STATE.NODE_STATE_SUCCESS;
            }
        }
    }        
}
