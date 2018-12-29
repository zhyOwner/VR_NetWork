using System.Collections.Generic;
using Common;
using UnityEngine;

public class RequestManager {

    private static RequestManager _instance;
    public static RequestManager Instance{
        get{
            if (_instance != null) return _instance;
            _instance = new RequestManager();
            return _instance;
        }
    }
    private Dictionary<ActionCode, BaseRequest> _requestDict = new Dictionary<ActionCode, BaseRequest>();

    public BaseRequest GetRequest(ActionCode code)
    {
        if (_requestDict.ContainsKey(code))
            return _requestDict[code];
        return null;
    }

    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        _requestDict.Add(actionCode, request);
    }
    public void RemoveRequest(ActionCode actionCode)
    {
        _requestDict.Remove(actionCode);
    }

    public void Tick(){
        foreach (var item in _requestDict.Values)
        {
            item.Tick();
        }
    }
    public void HandleReponse(ActionCode actionCode, string data)
    {
        BaseRequest request = _requestDict.TryGet<ActionCode, BaseRequest>(actionCode);
        if (request == null)
        {
            Debug.LogWarning("无法得到ActionCode[" + actionCode + "]对应的Request类");return;
        }
        request.Response(data);
    }
}