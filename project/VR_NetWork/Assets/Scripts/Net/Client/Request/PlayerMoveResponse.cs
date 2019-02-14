using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class PlayerMoveResponse : BaseRequest
{
    public override void Start()
    {
        requestCode = RequestCode.None;
        actionCode = ActionCode.MoveResponse;
        base.Start();
    }

    public override void OnResponse(string msg)
    {
        string[] playerMsgs = msg.Split('|');
        GameObject player = Main.GetPlayer(playerMsgs[0]);
        if (player = null) return;
        player.transform.position = Parse(playerMsgs[1]);
        player.transform.eulerAngles = Parse(playerMsgs[2]);
        //TODO 动画的同步 
    }

    private Vector3 Parse(string msg)
    {
        string[] str = msg.Split(',');
        float x = float.Parse(str[0]);
        float y = float.Parse(str[1]);
        float z = float.Parse(str[2]);
       return new Vector3(x, y , z);
    }
}
