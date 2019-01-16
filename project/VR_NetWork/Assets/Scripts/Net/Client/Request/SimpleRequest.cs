using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;


public class SimpleRequest : BaseRequest
{
    public override void Start(){

        requestCode = RequestCode.Game;
        actionCode = ActionCode.Simple;
        base.Start();
    }

    public void RegisterFun(string cmd , SimpleResponse response){
        if(!actionResponse.ContainsKey(cmd))
            actionResponse.Add(cmd , response);
    }


    public override void OnResponse(string msg){
        string[] msgs = msg.Split('|');
        if(!actionResponse.ContainsKey(msg)) return;
        SimpleResponse response = actionResponse[msg];
        response.action?.Invoke();
        response.actionStr?.Invoke(msgs[1]);
        response.actionStr2?.Invoke(msgs[1] , msgs[2]);
    }

    private Dictionary<string , SimpleResponse> actionResponse = new Dictionary<string, SimpleResponse>();
}

public class SimpleResponse{
    public Action action;
    public Action<string> actionStr;
    public Action<string , string> actionStr2;

    public SimpleResponse(Action action)
    {
        this.action = action;
    }

    public SimpleResponse(Action<string> actionStr)
    {
        this.actionStr = actionStr;
    }

    public SimpleResponse(Action<string , string> actionStr2)
    {
        this.actionStr2 = actionStr2;
    }

    public SimpleResponse(Action action , Action<string> actionStr , Action<string , string> actionStr2)
    {
        this.action = action;
        this.actionStr = actionStr;
        this.actionStr2 = actionStr2;
    }
}
