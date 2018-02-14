using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BT
{
    class BT_ACTION_TEST : BT_Action
    {
        public string logStr;
       

        public override void start(Context context, NodeContext nodeCtx)
        {
            base.start(context, nodeCtx);

            Debug.Log(logStr+"  : Start!" + Time.time);
        }

        public override void update(Context context, NodeContext nodeCtx)
        {
            base.update(context, nodeCtx);

            Debug.Log(logStr + "  : UpDate!" + Time.time);

            nodeCtx.CurNodeState = NODE_STATE.NODE_STATE_SUCCESS;
        }

        public override void end(Context context, NodeContext nodeCtx)
        {
            base.end(context, nodeCtx);
            Debug.Log(logStr + "  : End!" + Time.time);


        }
    }
}
