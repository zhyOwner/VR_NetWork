using System;
using UnityEngine;
using VRTK;
enum EventOperator
{
    GripClicked = 0,
    TouchpadPressed,
    TriggerClick,
    PointerEnter,
    PointerExit
}
/// <summary>
/// 此脚本只是添加射线的 Event 的 Listener
/// </summary>
public class ControllerPointerEventListener : MonoBehaviour {
    
    private void Awake() {
        VRTK_DestinationMarker maker = GetComponent<VRTK_DestinationMarker>();
        _controllerEvents = GetComponent<VRTK_ControllerEvents>();

        if(maker == null){

            Debug.Log("射线监听事件没找到...");
            return;
        }

        maker.DestinationMarkerEnter += new DestinationMarkerEventHandler(DoPointerIn);
        maker.DestinationMarkerExit += new DestinationMarkerEventHandler(DoPointerOut);
    }


    /// <summary>
    /// 添加移出事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DoPointerOut(object sender, DestinationMarkerEventArgs e)
    {
        Debug.Log("射线退出：" + e.target.name);
        BaseEvent _event = e.target.GetComponent<BaseEvent>();
        if(_event != null){
            _event.RemoveListener(_controllerEvents);
        }
    }

    /// <summary>
    /// 射线进入事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DoPointerIn(object sender, DestinationMarkerEventArgs e)
    {
        Debug.Log("射线打中：" + e.target.name);
        BaseEvent _event = e.target.GetComponent<BaseEvent>();
        if(_event != null){
            _event.AddListener(_controllerEvents);
        }
    }

    private VRTK_ControllerEvents _controllerEvents;
}