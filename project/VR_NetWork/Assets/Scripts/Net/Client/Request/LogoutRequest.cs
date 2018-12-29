using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class LogoutRequest : BaseRequest
{
    public Action action;

    public override void Start()
    {
        requestCode = RequestCode.Player;
        actionCode = ActionCode.Logout;
        base.Start();
    }

    public override void OnResponse(string userName)
    {
        Debug.Log("收到客户端下线消息：" + userName);
        //TODO
        (RequestManager.Instance.GetRequest(ActionCode.GetPlayer) as LinkPortRequest)?.RemovePlayer(userName);
        action?.Invoke();
    }
}
