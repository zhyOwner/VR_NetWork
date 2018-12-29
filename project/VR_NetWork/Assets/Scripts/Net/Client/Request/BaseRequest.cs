using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class BaseRequest : MonoBehaviour {

    protected RequestCode requestCode = RequestCode.None;
    protected ActionCode actionCode = ActionCode.None;
    

    public virtual void Start(){
        RequestManager.Instance.AddRequest(actionCode , this);
    }

    public virtual void SendRequest(string data){
        ClientManager.Instance.SendRequest(requestCode, actionCode, data);
    }

  
    public virtual void SendRequest(){}
    public virtual void OnResponse(string data){
    }

    public virtual void OnDestroy(){
        RequestManager.Instance.RemoveRequest(actionCode);
    }

    public void Response(string data)
    {
        _response.Enqueue(data);
    }

    public virtual void Tick()
    {
        if (_response.Count > 0)
        {
            OnResponse(_response.Dequeue());
        }
    }

    private Queue<string> _response = new Queue<string>();

}