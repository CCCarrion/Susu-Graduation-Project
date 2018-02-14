using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT
{
    class BT_Precondition:BT_Decorator
    {
        public BT_Condition preCon;

        public override void start(Context context, NodeContext nodeCtx)
        {
            base.start(context, nodeCtx);
            if (context.nodeContexts.ContainsKey(preCon))
            {
                context.nodeContexts[preCon].CurNodeState = NODE_STATE.NODE_STATE_INVALIDE;
            }
        }
        public override void update(Context context, NodeContext nodeCtx)
        {
            base.update(context, nodeCtx);

            NODE_STATE preConState = preCon.Tick(context);
            if(preConState != NODE_STATE.NODE_STATE_SUCCESS)
            {
                if(context.nodeContexts[task].CurNodeState == NODE_STATE.NODE_STATE_RUNNING)
                {
                    context.nodeContexts[task].CurNodeState = NODE_STATE.NODE_STATE_ABORTED;
                    task.Tick(context);
                }
            }
            else
            {
                if (context.nodeContexts[task].CurNodeState != NODE_STATE.NODE_STATE_RUNNING)
                {
                    context.nodeContexts[task].CurNodeState = NODE_STATE.NODE_STATE_INVALIDE;
                }
                task.Tick(context);
            }
            context.nodeContexts[preCon].CurNodeState = NODE_STATE.NODE_STATE_INVALIDE;

            nodeCtx.CurNodeState = context.nodeContexts[task].CurNodeState;
        }
    }
}
