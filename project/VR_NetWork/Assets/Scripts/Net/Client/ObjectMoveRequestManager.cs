using System.Collections.Generic;
using Common;
using UnityEngine;

public class ObjectMoveRequestManager : BaseRequest {
    
    public override void Start(){
        requestCode = RequestCode.Game;
        actionCode = ActionCode.ObjectMove;
        base.Start();
    }
    /// <summary>
    /// 添加 Object MoveRequest
    /// </summary>
    /// <param name="_name">自定义名字 ， 或者对象的名字</param>
    /// <param name="_request"></param>
    public void AddMoveRequest(string _name , ObjectMoveRequest _request){
        if(_nameObjRequest.ContainsKey(_name)) return;
        _nameObjRequest.Add(_name , _request);
    }

    /// <summary>
    /// 移除 Object MoveRequest
    /// </summary>
    /// <param name="_name"></param>
    public void RemoveRequest(string _name){
        if(!_nameObjRequest.ContainsKey(_name)) return;
        _nameObjRequest.Remove(_name);
    }



    /// <summary>
    /// 接受到来自服务器的 Object Move 移动的响应
    /// </summary>
    /// <param name="data">数据结构：string objectName | float position.x ,
    /// float position.y ,float position.z | rotation.x , rotation.y ,rotation.z</param>
    public override void OnResponse(string data){
        string[] objectInfo = data.Split('|');
        if(_nameObjRequest.ContainsKey(objectInfo[0]))
            _nameObjRequest[objectInfo[0]].OnResponse(data);
    }

    private Dictionary<string , ObjectMoveRequest> _nameObjRequest = new  Dictionary<string, ObjectMoveRequest>();
}