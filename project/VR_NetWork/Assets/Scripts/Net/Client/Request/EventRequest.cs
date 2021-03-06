using System.Collections.Generic;
using Common;
using UnityEngine;

/// <summary>
/// 射线事件请求基类
/// </summary>
public class EventRequest : BaseRequest {

    public override void Start(){
        requestCode = RequestCode.Game;
        actionCode = ActionCode.Pointer;
        base.Start();
    }

    public void AddEvent(string _name, BaseEvent _event){
        if(_eventDic.ContainsKey(_name)) return;
        _eventDic.Add(_name , _event);
        Debug.Log("添加交互对象： " + _name);
    }
    
    public void RemoveEvent(string _name){
        if(!_eventDic.ContainsKey(_name)) return;
        _eventDic.Remove(_name);
        Debug.Log("移除交换对象： " + _name);
    }

    public override void OnResponse(string data){
        Debug.Log("收到交换相应： " + data);
        string[] _nameEvent = data.Split('|');
        if(!_eventDic.ContainsKey(_nameEvent[0])) return;
        _eventDic[_nameEvent[0]].OnResponse(data);
    }

    /// <summary>
    /// 保存所有 手柄射线事件的 字典
    /// </summary>
    /// <typeparam name="string">name</typeparam>
    /// <typeparam name="BaseEvent">BaseEvent</typeparam>
    /// <returns></returns>
    private Dictionary<string , BaseEvent> _eventDic = new Dictionary<string, BaseEvent>();
    
}