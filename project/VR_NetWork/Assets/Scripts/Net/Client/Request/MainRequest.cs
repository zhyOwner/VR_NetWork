using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainRequest : BaseRequest
{
    public GameObject buttonListTransform;
    public List<Button> TaskButtons;


    public override void Start()
    {
        requestCode = RequestCode.Main;
        actionCode = ActionCode.Task;
        base.Start();

        /* 判断当前选择的端口是否是监控端 */
        if (ClientManager.Port == Port.Monitor)//监控端
        {
            /* 添加 button 事件 */
            foreach (Button taskButton in TaskButtons)
            {
                string taskName = taskButton.name;
                taskButton.onClick.AddListener(delegate ()
                {
                    AddButtonEvent(taskName);
                });
            }
        }
        else//其他端口
        {
            //关掉任务列表
            buttonListTransform.SetActive(false);
        }
       
    }

    /// <summary>
    /// 任务选择事件
    /// </summary>
    /// <param name="task"></param>
    private void AddButtonEvent(string task)
    {
        /* 发送任务选择场景消息 */
        SendRequest(task);
    }

    public override void OnResponse(string msg)
    {
        //加载场景
        SceneManager.LoadScene(msg + "Scene");
    }

   
}
