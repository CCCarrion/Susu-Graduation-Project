using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT
{
    //sequence
    public class BT_Sequence : BT_Composite
    {
        public class BT_SequenceContext : NodeContext
        {
            public int curPos;
        }

        public override NodeContext CreateNodeContext(Context context)
        {
            return new BT_SequenceContext();
        }

        public override void start(Context context, NodeContext nodeCtx)
        {
            base.start(context, nodeCtx);
            (nodeCtx as BT_SequenceContext).curPos = 0;
            //(nodeCtx as BT_SequenceContext).CurNodeState = NODE_STATE.NODE_STATE_RUNNING;
        }

        public override void update(Context context, NodeContext nodeCtx)
        {
            base.update(context, nodeCtx);

            BT_SequenceContext nodectx = nodeCtx as BT_SequenceContext;
            NODE_STATE childState = NODE_STATE.NODE_STATE_INVALIDE;
            for (; nodectx.curPos < ChildrenNode.Count; nodectx.curPos++)
            {
                childState = ChildrenNode[nodectx.curPos].Tick(context);
                if (childState != NODE_STATE.NODE_STATE_SUCCESS)
                    break;
            }
            nodectx.CurNodeState = childState;
        }       
    }

    //selector
    public class BT_Selector : BT_Composite
    {
        public class BT_SelectorContext : NodeContext
        {
            public int selectPos;
        }

        public override NodeContext CreateNodeContext(Context context)
        {
            return new BT_SelectorContext();
        }

        public override void start(Context context, NodeContext nodeCtx)
        {
            base.start(context, nodeCtx);
            (nodeCtx as BT_SelectorContext).selectPos = 0;
            //(nodeCtx as BT_SelectorContext).CurNodeState = NODE_STATE.NODE_STATE_RUNNING;
        }

        public override void update(Context context, NodeContext nodeCtx)
        {
            base.update(context, nodeCtx);

            BT_SelectorContext nodectx = nodeCtx as BT_SelectorContext;
            NODE_STATE childState = NODE_STATE.NODE_STATE_INVALIDE;
            for (; nodectx.selectPos < ChildrenNode.Count; nodectx.selectPos++)
            {
                childState = ChildrenNode[nodectx.selectPos].Tick(context);
                if (childState != NODE_STATE.NODE_STATE_FAILURE)
                    break;
            }
            nodectx.CurNodeState = childState;
        }       
    }

    //Parallel
    public class BT_Parallel : BT_Composite
    {
        public enum  BT_Parallel_Policy
        {
            REQUIRE_ONE_FAIL = 0,
            REQUIRE_ONE_SUCCESS,
            REQUIRE_ALL_FAIL,
            REQUIRE_ALL_SUCCESS
        }

        public BT_Parallel_Policy policy;


        //public override void start(Context context, NodeContext nodeCtx)
        //{
        //    nodeCtx.CurNodeState = NODE_STATE.NODE_STATE_RUNNING;
        //}

        public override void update(Context context, NodeContext nodeCtx)
        {
            base.update(context,nodeCtx);

            bool bHasFail = false;
            bool bHasSucc = false;
            bool bHasRunning = false;

            NODE_STATE childState = NODE_STATE.NODE_STATE_INVALIDE;
            foreach (var node in ChildrenNode)
            {
                if (context.nodeContexts.ContainsKey(node))
                {
                    if (context.nodeContexts[node].CurNodeState == NODE_STATE.NODE_STATE_SUCCESS)
                    {
                        bHasSucc = true;
                        continue;
                    }
                    if (context.nodeContexts[node].CurNodeState == NODE_STATE.NODE_STATE_FAILURE)
                    {
                        bHasFail = true;
                        continue;
                    }
                }
                childState = node.Tick(context);
                bHasFail |= childState == NODE_STATE.NODE_STATE_FAILURE;
                bHasSucc |= childState == NODE_STATE.NODE_STATE_SUCCESS;
                bHasFail |= childState == NODE_STATE.NODE_STATE_RUNNING;
            }

            if (policy == BT_Parallel_Policy.REQUIRE_ALL_SUCCESS)
            {
                nodeCtx.CurNodeState = bHasRunning ? NODE_STATE.NODE_STATE_RUNNING
                                     : bHasFail ? NODE_STATE.NODE_STATE_FAILURE
                                     : NODE_STATE.NODE_STATE_SUCCESS;
            }
            if (policy == BT_Parallel_Policy.REQUIRE_ALL_FAIL)
            {
                nodeCtx.CurNodeState = bHasRunning ? NODE_STATE.NODE_STATE_RUNNING 
                                     : bHasSucc ? NODE_STATE.NODE_STATE_SUCCESS 
                                     : NODE_STATE.NODE_STATE_FAILURE;
            }

            if (policy == BT_Parallel_Policy.REQUIRE_ONE_FAIL)
            {
                nodeCtx.CurNodeState = bHasFail ? NODE_STATE.NODE_STATE_FAILURE
                                     : bHasRunning ? NODE_STATE.NODE_STATE_RUNNING
                                     : NODE_STATE.NODE_STATE_SUCCESS;
            }

            if (policy == BT_Parallel_Policy.REQUIRE_ONE_SUCCESS)
            {
                nodeCtx.CurNodeState = bHasSucc ? NODE_STATE.NODE_STATE_SUCCESS
                                     : bHasRunning ? NODE_STATE.NODE_STATE_RUNNING
                                     : NODE_STATE.NODE_STATE_FAILURE;
            }
        }

        public override void end(Context context, NodeContext nodeCtx)
        {
            base.end(context, nodeCtx);
            AbortedAllRunningChildren(context);
        }
    }
}
