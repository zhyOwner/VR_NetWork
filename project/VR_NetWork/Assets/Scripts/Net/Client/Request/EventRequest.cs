using System.Collections.Generic;
using Common;
using UnityEngine;

public class EventRequest : BaseRequest {

    public override void Start(){
        requestCode = RequestCode.Game;
        actionCode = ActionCode.Pointer;
        base.Start();
    }

    public void AddEvent(string _name, BaseEvent _event){
        if(_eventDic.ContainsKey(_name)) return;
        _eventDic.Add(_name , _event);
    }
    
    public void RemoveEvent(string _name){
        if(!_eventDic.ContainsKey(_name)) return;
        _eventDic.Remove(_name);
    }

    public override void OnResponse(string data){
        string[] _nameEvent = data.Split('|');
        if(!_eventDic.ContainsKey(_nameEvent[0])) return;
        _eventDic[_nameEvent[0]].OnResponse(_nameEvent[0]);
    }

    private Dictionary<string , BaseEvent> _eventDic = new Dictionary<string, BaseEvent>();
    
}