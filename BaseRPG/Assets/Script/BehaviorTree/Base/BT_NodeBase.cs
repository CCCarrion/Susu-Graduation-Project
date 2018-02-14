using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor.VersionControl;

namespace BT
{
    public enum NODE_STATE
    {
        NODE_STATE_INVALIDE = 0,
        NODE_STATE_SUCCESS,
        NODE_STATE_FAILURE,
        NODE_STATE_RUNNING,
        NODE_STATE_ABORTED
    }

    public class Context
    {
        public Dictionary<BT_NodeBase,NodeContext> nodeContexts = new Dictionary<BT_NodeBase, NodeContext>();
        public HashSet<string> flagSets  = new HashSet<string>();
        public Dictionary<string, object> blackBoard = new Dictionary<string, object>();

    }

    public class NodeContext
    {

        public NODE_STATE CurNodeState = NODE_STATE.NODE_STATE_INVALIDE;
    }

    public abstract class BT_NodeBase
    {

        public virtual NodeContext CreateNodeContext(Context context)
        {
            return new NodeContext();
        }

        public virtual void start(Context context, NodeContext nodeCtx)
        {
        }

        public virtual void update(Context context, NodeContext nodeCtx)
        {

        }

        public virtual void end(Context context, NodeContext nodeCtx) { }

        public virtual NODE_STATE Tick(Context context)
        {
            NodeContext nodectx;
            if (!context.nodeContexts.TryGetValue(this, out nodectx))
            {
                context.nodeContexts[this] = CreateNodeContext(context);
                nodectx = context.nodeContexts[this];
            }

            if (nodectx.CurNodeState == NODE_STATE.NODE_STATE_INVALIDE)
            {
                nodectx.CurNodeState = NODE_STATE.NODE_STATE_RUNNING;
                start(context, nodectx);
            }

            if (nodectx.CurNodeState == NODE_STATE.NODE_STATE_RUNNING)
            {
                update(context, nodectx);
            }

            if (nodectx.CurNodeState != NODE_STATE.NODE_STATE_RUNNING)
            {
                end(context, nodectx);
            }

            return nodectx.CurNodeState;
        }
    }

    public abstract class BT_Composite : BT_NodeBase
    {
        public List<BT_NodeBase> ChildrenNode = new List<BT_NodeBase>();

        protected void InvalidAllChildren(Context context)
        {
            foreach (var node in ChildrenNode)
            {
                if (context.nodeContexts.ContainsKey(node))
                {
                    context.nodeContexts[node].CurNodeState = NODE_STATE.NODE_STATE_INVALIDE;
                }
            }
        }

        protected void AbortedAllRunningChildren(Context context)
        {
            foreach (var node in ChildrenNode)
            {
                if (context.nodeContexts.ContainsKey(node))
                {
                    if (context.nodeContexts[node].CurNodeState == NODE_STATE.NODE_STATE_RUNNING)
                    {
                        context.nodeContexts[node].CurNodeState = NODE_STATE.NODE_STATE_ABORTED;
                        node.Tick(context);
                    }
                }
            }
        }

        public override NODE_STATE Tick(Context context)
        {
            NodeContext nodectx;
            if (!context.nodeContexts.TryGetValue(this, out nodectx))
            {
                context.nodeContexts[this] = CreateNodeContext(context);
                nodectx = context.nodeContexts[this];
            }

            if (nodectx.CurNodeState == NODE_STATE.NODE_STATE_INVALIDE)
            {
                start(context, nodectx);
                //Start时候子节点也全部设置未访问
                InvalidAllChildren(context);
                nodectx.CurNodeState = NODE_STATE.NODE_STATE_RUNNING;
            }

            if (nodectx.CurNodeState == NODE_STATE.NODE_STATE_RUNNING)
            {
                update(context, nodectx);
            }
            //如果舍弃 子节点在运行中的也需要舍弃
            if (nodectx.CurNodeState == NODE_STATE.NODE_STATE_ABORTED)
            {
                AbortedAllRunningChildren(context);
            }

            if (nodectx.CurNodeState != NODE_STATE.NODE_STATE_RUNNING)
            {
                end(context, nodectx);             
            }

            return nodectx.CurNodeState;
        }
    }

    public abstract class BT_Condition : BT_NodeBase
    {
        public virtual bool Evaluate(Context context)
        {
            return true;
        }

        public override void start(Context context, NodeContext nodeCtx)
        {
            base.start(context, nodeCtx);
            nodeCtx.CurNodeState = Evaluate(context)?NODE_STATE.NODE_STATE_SUCCESS:NODE_STATE.NODE_STATE_FAILURE;
        }

    }

    public abstract class BT_Action : BT_NodeBase
    {

        public override void update(Context context, NodeContext nodeCtx)
        {
            base.update(context, nodeCtx);           
        }
    }

    public abstract class BT_Decorator : BT_NodeBase
    {
        public BT_NodeBase task;

        public override void start(Context context, NodeContext nodeCtx)
        {
            base.start(context, nodeCtx);
            if (context.nodeContexts.ContainsKey(task))
            {
                context.nodeContexts[task].CurNodeState = NODE_STATE.NODE_STATE_INVALIDE;
            }
        }

        public override void update(Context context, NodeContext nodeCtx)
        {
            base.update(context, nodeCtx);
           
        }

        public override void end(Context context, NodeContext nodeCtx)
        {
            base.end(context,nodeCtx);
            if (nodeCtx.CurNodeState == NODE_STATE.NODE_STATE_ABORTED)
            {
                if(context.nodeContexts.ContainsKey(task))
                {
                    context.nodeContexts[task].CurNodeState = NODE_STATE.NODE_STATE_ABORTED;
                    task.Tick(context);
                } 
            }
        }
    }
}
