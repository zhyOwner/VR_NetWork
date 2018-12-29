using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class PlayerMoveRequest : BaseRequest
{
    [HideInInspector]public string id;
    [HideInInspector]public string animator;
    [HideInInspector]public float rate = 30;//同步发送位置的速率
    public override void Start()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.Move;
       
        base.Start();


        /* 测试阶段不需要同步 */
        if(Main.isTest) return;

        id = ClientManager.Instance.ID;
        /* 延迟两秒发送 */
        InvokeRepeating("SyncPlayer" , 2 , 1/rate);
    }


    private void SyncPlayer()
    {
        //本地角色需要同步的数据 id Position Rotation animator
        SendRequest(id + "|" + transform.position.x+"," + transform.position.y + "," +
                    transform.position.z + "|" + transform.eulerAngles.x + "," + transform.eulerAngles.y +
                    "," + transform.eulerAngles.z + "|"+ animator);

    }
}
