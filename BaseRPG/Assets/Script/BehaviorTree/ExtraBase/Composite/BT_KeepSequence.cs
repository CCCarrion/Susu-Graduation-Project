﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT
{
    class BT_KeepSequence : BT_Composite
    {
        public override void update(Context context, NodeContext nodeCtx)
        {
            base.update(context, nodeCtx);

            int step = 0;
            foreach (BT_NodeBase node in ChildrenNode)
            {
                NodeContext childCtx = context.nodeContexts[node];

                childCtx.CurNodeState = childCtx.CurNodeState == NODE_STATE.NODE_STATE_RUNNING ? NODE_STATE.NODE_STATE_RUNNING : NODE_STATE.NODE_STATE_INVALIDE;

                node.Tick(context);
                step++;
                if (childCtx.CurNodeState != NODE_STATE.NODE_STATE_SUCCESS)
                {
                    break;
                }
            }

            for (; step < ChildrenNode.Count; step++)
            {
                NodeContext childCtx = context.nodeContexts[ChildrenNode[step]];

                if (childCtx.CurNodeState == NODE_STATE.NODE_STATE_RUNNING)
                {
                    childCtx.CurNodeState = NODE_STATE.NODE_STATE_ABORTED;
                    ChildrenNode[step].Tick(context);
                }
                childCtx.CurNodeState = NODE_STATE.NODE_STATE_INVALIDE;
            }
        }

    }
}
