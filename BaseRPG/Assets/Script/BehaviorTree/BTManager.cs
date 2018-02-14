using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BT
{
    public class BTManager
    {
        private static BTManager instance_;
        public static BTManager Instance
        {
            get
            {
                if (instance_ == null)
                {
                    instance_ = new BTManager();
                }

                return instance_;
            }
        }

        private Dictionary<int ,BT_NodeBase> BTDic = new Dictionary<int, BT_NodeBase>();


        // 加载机制暂时不做  此时为临时加载代码
        public BT_NodeBase GetBehaviorTreeRoot(int aiType)
        {
            if (aiType == 10001) // 小兵 AI
            {
                if (!BTDic.ContainsKey(aiType))
                {
                    //AddBT2Dic_10001();
                    BTDic.Add(10001,AddBT_TEST());
                }
                return BTDic[aiType];
            }




            return null;
        }

        //临时代码
        /*
        private void AddBT2Dic_10001()
        {
            //设置 root
            BT_Loop root = new BT_Loop();
            root.LoopCount = -1;
            root.loopStopSetting = BT_Loop.LoopStopCondition.NONE;
            //KeepSelector
            BT_KeepSelector layer1 = new BT_KeepSelector();
            root.task = layer1;

            //BeAttacked 
            BT_Sequence beAttacked = new BT_Sequence();
            layer1.ChildrenNode.Add(beAttacked);

            CheckFlag checkAttacked = new CheckFlag();
            beAttacked.ChildrenNode.Add(checkAttacked);
            checkAttacked.tarFlag = RoleStateFlag.BeAttacked;

            ClearFlag clearflag = new ClearFlag();
            beAttacked.ChildrenNode.Add(clearflag);
            clearflag.flag = RoleStateFlag.BeAttacked;

            SetAnimation SA = new SetAnimation();
            beAttacked.ChildrenNode.Add(SA);
            SA.aniID = AnimationID.BeAttacked;

            BT_WaitForSeconds wait4second = new BT_WaitForSeconds();
            beAttacked.ChildrenNode.Add(wait4second);
            wait4second.totalSeconds = 1;

            //Die
            BT_Sequence die = new BT_Sequence();
            layer1.ChildrenNode.Add(die);

            CheckHPRange checkHP = new CheckHPRange();
            die.ChildrenNode.Add(checkHP);
            checkHP.Min = 1;
            checkHP.Max = -1;

            SetAnimation SADie = new SetAnimation();
            die.ChildrenNode.Add(SADie);
            SADie.aniID = AnimationID.Die;

            BT_WaitForSeconds ws1p5 = new BT_WaitForSeconds();
            die.ChildrenNode.Add(ws1p5);
            ws1p5.totalSeconds = 1.5f;

            //Attack
            BT_Sequence attack = new BT_Sequence();
            layer1.ChildrenNode.Add(attack);

            CheckTargetIsAround checkTar = new CheckTargetIsAround();
            attack.ChildrenNode.Add(checkTar);
            checkTar.distance = 1f;

            SetAnimation SAAttack = new SetAnimation();
            attack.ChildrenNode.Add(SAAttack);
            SAAttack.aniID = AnimationID.Attack;

            BT_WaitForSeconds ws0p5 = new BT_WaitForSeconds();
            attack.ChildrenNode.Add(ws0p5);
            ws0p5.totalSeconds = 0.5f;

            CreateSkillEffect atkPrefab = new CreateSkillEffect();
            attack.ChildrenNode.Add(atkPrefab);
            atkPrefab.prefabName = "Attack1";

            //WalkAround
            BT_Sequence walkAround = new BT_Sequence();
            layer1.ChildrenNode.Add(walkAround);

            ReverseResult revrst = new ReverseResult();

            CheckTargetIsAround checkAround = new CheckTargetIsAround();

            SetAnimation SAStan = new SetAnimation();

            BT_WaitForSeconds w1s = new BT_WaitForSeconds();

            ComputePatrolPos patrol = new ComputePatrolPos();

            SetAnimation ASWalk = new SetAnimation();

            MoveTo move1 = new MoveTo();


            //Run2Player
            BT_Sequence run2p = new BT_Sequence();
            layer1.ChildrenNode.Add(run2p);

            CheckTargetIsAround ckia = new CheckTargetIsAround();

            BT_Parallel parallel = new BT_Parallel();


            ComputeTargetPos tarPos = new ComputeTargetPos();

            BT_Sequence seq = new BT_Sequence();

            SetAnimation SARun = new SetAnimation();

            MoveTo move2 = new MoveTo();

        }
        */

        private BT_NodeBase AddBT_TEST()
        {
            //设置 root
            BT_Loop root = new BT_Loop();
            root.LoopCount = -1;
            root.loopStopSetting = BT_Loop.LoopStopCondition.NONE;

            BT_Sequence layer1 = new BT_Sequence();
            root.task = layer1;

            BT_ACTION_TEST log1 = new BT_ACTION_TEST();
            log1.logStr = "Log1";
            layer1.ChildrenNode.Add(log1);

            BT_WaitForSeconds waiter1 = new BT_WaitForSeconds();
            waiter1.totalSeconds = 3;
            layer1.ChildrenNode.Add(waiter1);

            BT_CONDITION_TEST checkTrue = new BT_CONDITION_TEST();
            checkTrue.bSwitch = true;
            layer1.ChildrenNode.Add(checkTrue);

            BT_ACTION_TEST log2 = new BT_ACTION_TEST();
            log2.logStr = "Log2";
            layer1.ChildrenNode.Add(log2);

            BT_CONDITION_TEST checkFalse = new BT_CONDITION_TEST();
            checkFalse.bSwitch = false;
            layer1.ChildrenNode.Add(checkFalse);

            BT_ACTION_TEST log3 = new BT_ACTION_TEST();
            log3.logStr = "Log3";
            layer1.ChildrenNode.Add(log3);

            return root;
        }
    }
}
