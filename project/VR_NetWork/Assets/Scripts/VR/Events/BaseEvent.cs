using System;
using UnityEngine;
using VRTK;

/// <summary>
/// 射线监听事件的基类
/// </summary>
public class BaseEvent : MonoBehaviour {
    
    protected virtual void Awake() {
        _eventRequest = Main.Instance.GetComponent<EventRequest>();
        _name = transform.name;
    }


    protected virtual void OnEnable(){
         _eventRequest.AddEvent(_name , this);
    }

    protected virtual void OnDisable(){
         _eventRequest.RemoveEvent(_name);
    }

    /// <summary>
    /// 移除监听事件
    /// </summary>
    /// <param name="_controllerEvents"></param>
    public void RemoveListener(VRTK_ControllerEvents _controllerEvents){
        _controllerEvents.TriggerClicked -= OnTriggerClick;
        _controllerEvents.TouchpadPressed -= OnTouchpadPressed;
        _controllerEvents.GripClicked -= OnGripClicked;
        OnPointerExit();
    }

    /// <summary>
    /// 添加监听事件
    /// </summary>
    /// <param name="_controllerEvents"></param>
    public void AddListener(VRTK_ControllerEvents _controllerEvents){
        _controllerEvents.TriggerClicked += OnTriggerClick;
        _controllerEvents.TouchpadPressed += OnTouchpadPressed;
        _controllerEvents.GripClicked += OnGripClicked;
        OnPointerEnter();
    }


    /// <summary>
    /// 射线打中对象握持键点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public virtual void OnGripClicked(object sender, ControllerInteractionEventArgs e)
    {
        SendRequest((int)EventOperator.GripClicked + "|" + "OnGripClicked");
    }


    /// <summary>
    /// 射线打中对象触摸键按压事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public virtual void OnTouchpadPressed(object sender, ControllerInteractionEventArgs e)
    {
        SendRequest((int)EventOperator.TouchpadPressed + "|" + "OnTouchpadPressed");
    }

    /// <summary>
    /// 射线打中对象扣扳机事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public virtual void OnTriggerClick(object sender, ControllerInteractionEventArgs e)
    {
        SendRequest((int)EventOperator.TriggerClick + "|" + "OnTriggerClick");
    }

    /// <summary>
    /// 射线打中事件
    /// </summary>
    public virtual void OnPointerEnter(){
        SendRequest((int)EventOperator.PointerEnter + "|" + "OnPointerEnter");
    }

    /// <summary>
    /// 射线退出事件
    /// </summary>
    public virtual void OnPointerExit()
    {
        SendRequest((int)EventOperator.PointerExit + "|" + "OnPointerExit");
    }


    /// <summary>
    /// 来自服务的的响应
    /// </summary>
    /// <param name="msg">EventOperator|事件</param>
    public virtual void OnResponse(string msg){
        string[] op_msg = msg.Split('|');
        EventOperator op = (EventOperator)int.Parse(op_msg[1]);
        switch (op)
        {
            case EventOperator.TriggerClick:
                TriggerClick(op_msg[2]);
                break;
            case EventOperator.TouchpadPressed:
                TouchpadPressed(op_msg[2]);
                break;
            case EventOperator.GripClicked:
                GripClicked(op_msg[2]);
                break;
            case EventOperator.PointerEnter:
                PointerEnter(op_msg[2]);
                break;
            case EventOperator.PointerExit:
                PointerExit(op_msg[2]);
                break;
            default:
                break;
        }
    }


    private void SendRequest(string msg){
        _eventRequest.SendRequest(_name + "|" + msg);
    }

    /// <summary>
    /// 来自服务器的 TriggerClick 响应
    /// </summary>
    public virtual void TriggerClick(string msg){

    }

    /// <summary>
    /// 来自服务器的 TouchpadPressed 响应
    /// </summary>
    public virtual void TouchpadPressed(string msg){

    }

    /// <summary>
    /// 来自服务器的 GripClicked 响应
    /// </summary>
    public virtual void GripClicked(string msg){

    }

    /// <summary>
    /// 来自服务器的 PointerEnter 响应
    /// </summary>
    public virtual void PointerEnter(string msg){

    }

    /// <summary>
    /// 来自服务器的 PointerExit 响应
    /// </summary>
    public virtual void PointerExit(string msg){

    }

   private EventRequest _eventRequest;
   private string _name;

}