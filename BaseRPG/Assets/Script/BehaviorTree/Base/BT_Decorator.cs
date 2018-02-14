using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT
{
    //Loop
    public class BT_Loop : BT_Decorator
    {
        public enum LoopStopCondition
        {
            NONE,
            ONCE_FAIL,
            ONCE_SUCCESS,
        }
        public class BT_LoopContext : NodeContext
        {
            public int curCount;
        }


        public int LoopCount;
        public LoopStopCondition loopStopSetting;
        public override NodeContext CreateNodeContext(Context context)
        {
            return new BT_LoopContext();
        }

        public override void start(Context context, NodeContext nodeCtx)
        {
            base.start(context, nodeCtx);
            (nodeCtx as BT_LoopContext).curCount = 0;
            
        }

        public override void update(Context context, NodeContext nodeCtx)
        {
            base.update(context, nodeCtx);

            BT_LoopContext nodectx = nodeCtx as BT_LoopContext;
            NODE_STATE childState = NODE_STATE.NODE_STATE_INVALIDE;
            while(nodectx.curCount != LoopCount)
            {
                childState = task.Tick(context);
                if (childState == NODE_STATE.NODE_STATE_RUNNING)
                {
                    break;
                }
                nodectx.curCount++;
                if (childState == NODE_STATE.NODE_STATE_FAILURE && loopStopSetting == LoopStopCondition.ONCE_FAIL)
                {
                    break;
                }
                if (childState == NODE_STATE.NODE_STATE_SUCCESS && loopStopSetting == LoopStopCondition.ONCE_SUCCESS)
                {
                    break;
                }

                if (context.nodeContexts.ContainsKey(task))
                {
                    if (context.nodeContexts[task].CurNodeState != NODE_STATE.NODE_STATE_RUNNING)
                    {
                        context.nodeContexts[task].CurNodeState = NODE_STATE.NODE_STATE_INVALIDE;
                    }
                }
            }
            nodeCtx.CurNodeState = childState;
        }

    }

    //结果反向
    public class ReverseResult : BT_Decorator
    {
        public override void end(Context context, NodeContext nodeCtx)
        {
            base.end(context, nodeCtx);

            nodeCtx.CurNodeState =
                nodeCtx.CurNodeState == NODE_STATE.NODE_STATE_FAILURE
                    ? NODE_STATE.NODE_STATE_SUCCESS
                    : nodeCtx.CurNodeState == NODE_STATE.NODE_STATE_SUCCESS
                        ? NODE_STATE.NODE_STATE_FAILURE
                        : nodeCtx.CurNodeState;
        }
    }
}
