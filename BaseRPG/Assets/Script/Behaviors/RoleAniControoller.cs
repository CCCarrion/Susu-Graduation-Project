using UnityEngine;
using System.Collections;


public struct AnimationConst
{
    //状态机 ActionID
    public const int ACTION_IDLE = 0;
    public const int ACTION_MOVE = ACTION_IDLE + 1;
    public const int ACTION_ATTACK = ACTION_IDLE + 2;
    public const int ACTION_DIE = ACTION_IDLE + 3;

    public const int SKILL_1 = ACTION_IDLE + 4;
    public const int SKILL_2 = SKILL_1 + 1;
    public const int SKILL_3 = SKILL_1 + 2;
    public const int SKILL_4 = SKILL_1 + 3;

    //状态名
    public const string STATE_IDLE = "Idle";
    public const string STATE_MOVE = "Move";
    public const string STATE_ATTACK = "Attack";
    public const string STATE_DIE = "Die";
    public const string STATE_SKILL1 = "Skill1";
    public const string STATE_SKILL2 = "Skill2";
    public const string STATE_SKILL3 = "Skill3";
    public const string STATE_SKILL4 = "Skill4";

}


public class RoleAniControoller : MonoBehaviour
{
    public AnimationClip[] baseActions;
    public AnimationClip[] skillActions;

    private Animator aniStateMachine;
    private AnimatorOverrideController aniOverride;

	// Use this for initialization
	void Start ()
	{
	    aniStateMachine = GetComponent<Animator>() ?? gameObject.AddComponent<Animator>();

	    aniOverride = new AnimatorOverrideController();
	    aniOverride.runtimeAnimatorController = aniStateMachine.runtimeAnimatorController;
	    aniStateMachine.runtimeAnimatorController = aniOverride;

	    LoadAnimationClip();
	}

    //装载动作
    void LoadAnimationClip()
    {
        //加载AnimationClip

        
        //重载动作
        aniOverride[AnimationConst.STATE_IDLE] = baseActions[AnimationConst.ACTION_IDLE];
        aniOverride[AnimationConst.STATE_MOVE] = baseActions[AnimationConst.ACTION_MOVE];
        aniOverride[AnimationConst.STATE_ATTACK] = baseActions[AnimationConst.ACTION_ATTACK];
        aniOverride[AnimationConst.STATE_DIE] = baseActions[AnimationConst.ACTION_DIE];


        aniOverride[AnimationConst.STATE_SKILL1] = skillActions[0];
        aniOverride[AnimationConst.STATE_SKILL2] = skillActions[1];
        aniOverride[AnimationConst.STATE_SKILL3] = skillActions[2];
        aniOverride[AnimationConst.STATE_SKILL4] = skillActions[3];
    }



}
