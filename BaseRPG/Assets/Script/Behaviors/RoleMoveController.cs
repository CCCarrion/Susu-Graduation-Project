using UnityEngine;
using System.Collections;

public class RoleMoveController : MonoBehaviour
{

    private NavMeshAgent naviMover;

    void Awake()
    {
        naviMover = GetComponent<NavMeshAgent>();
        if (null == naviMover)
        {
            naviMover = gameObject.AddComponent<NavMeshAgent>();
        }
    }


    // 移动目标到指定位置
    public void MoveToPosition( Vector3 pos)
    {
        naviMover.destination = pos;
    }



}
