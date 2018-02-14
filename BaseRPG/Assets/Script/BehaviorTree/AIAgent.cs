using UnityEngine;
using System.Collections;
using BT;

public static class RoleStateFlag
{
    public static readonly string BeAttacked = "1";
}
public static class AnimationID
{
    public static readonly int BeAttacked = 1;
    public static readonly int Die = 2;
    public static readonly int Attack = 3;

}

public class RoleContext : Context
{

    //Components
    public GameObject obj;
    public Animator animator;
    public NavMeshAgent naviAgent;

    
}

public class AIAgent : MonoBehaviour {

    

    private RoleContext roleContext_;
    private BT_NodeBase root_;

    void Start()
    {
        //Test
        InitAgent(10001);
        roleContext_ = new RoleContext();
    }


    public bool InitAgent(int aiType)
    {

        root_ = BTManager.Instance.GetBehaviorTreeRoot(aiType);
        return true;
    }

	// Update is called once per frame
	void Update ()
	{
	    root_.Tick(roleContext_);
	}
}
