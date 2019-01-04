using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.UI;

public class LinkPortRequest : BaseRequest
{
    public Image monitorImage;
    public Image operatorImage;
    public Image roamImage;
    public Text roamCountText;
    

    public override void Start()
    {
        actionCode = ActionCode.GetPlayer;
        requestCode = RequestCode.Player;
        base.Start();

        monitorImage.color = Color.gray;
        operatorImage.color = Color.gray;
        roamImage.color = Color.gray;
        roamCountText.text = _roamCount.ToString();

        DontDestroyOnLoad(gameObject);
        SendRequest("getplayer");
    }

    /// <summary>
    /// 接受其他端口登录的响应
    /// </summary>
    /// <param name="userName">id|Port</param>
    public override void OnResponse(string userName)
    {
        Debug.Log("收到客户端上线消息：" + userName);
        if (_isGet)
        {
            _isGet = false;
            string[] players = userName.Split('|');
            for (int i = 1; i < players.Length; i++)
            {
                string[] userMsg = players[i].Split('-');
                string _userName = userMsg[0] + "|" + userMsg[1];
                AddPlayer(_userName);
            }
           
        }
        else
        {
            AddPlayer(userName);
        }

       
    }

    /// <summary>
    /// 移除登出的端口的响应
    /// </summary>
    /// <param name="userName"></param>
    public void RemovePlayer(string userName)
    {
        string[] userMsg = userName.Split('|');
        switch (userMsg[1])
        {
            case "Monitor":
                monitorImage.color = Color.gray;
                break;
            case "Operator":
                operatorImage.color = Color.gray;
                break;
            case "Roaming":
                _roamCount--;
                if (_roamCount <= 0)
                {
                    roamImage.color = Color.gray;
                    _roamCount = 0;
                }
                roamCountText.text = _roamCount.ToString();
                break;
        }

        /* 移除同步角色对象 */
        RemovePlayerObj(userMsg[0]);
    }

    /// <summary>
    /// 添加登录的端口
    /// </summary>
    /// <param name="port"></param>
    private void AddPlayer(string userName)
    {
        switch (userName.Split('|')[1])
        {
            case "Monitor":
                monitorImage.color = Color.green;
                break;
            case "Operator":
                operatorImage.color = Color.green;
                break;
            case "Roaming":
                roamImage.color = Color.green;
                _roamCount++;
                roamCountText.text = _roamCount.ToString();
                break;
        }

        CreatePlayer(userName);
    }


    private void CreatePlayer(string playerMsg)
    {
        string[] msg = playerMsg.Split('|');
        if(msg[0] == ClientManager.Instance.ID) return;
        if (Main._players.ContainsKey(msg[0])) return;
        GameObject player = null;
        switch (msg[1])
        {
            case "Operator":
                player = GameObject.Instantiate(Resources.Load<GameObject>("VR/Player/Operator"));
                break;
            case "Roaming":
                player = GameObject.Instantiate(Resources.Load<GameObject>("VR/Player/Roam"));
                break;
            default:
                break;
        }
        if(player == null) return;
        DontDestroyOnLoad(player);
        /* 关于 Player 同步的想法
         1. 如何自动适配 所有的端口哪个该同步其他端口的角色 哪个该发送自己角色的位置
             1). 所有的 HTC 端口发送自己端口的位置的对象为  Camera eye 对象的位置
             （可以用两个 ActionCode 区别一个为发送消息 一个为响应消息 意思是 负责发消息和响应消息的功能分开 
              因为 本地端口的角色只负责发送自己的位置，本地端口的其他角色只负责同步其他端口的位置）
         2.监控端是不需要有角色的 （只需要同步其他端口的角色）
         */
        //将 Player 添加到字典中统一管理
        Main._players.Add(msg[0], player);
    }

    private void RemovePlayerObj(string id)
    {
        if(!Main._players.ContainsKey(id)) return;
        DestroyImmediate(Main._players[id]);
        Main._players.Remove(id);
    }

    private int _roamCount;//登录的漫游端的个数
    private bool _isGet = true; //是否是主动获取 已登录的端口的信息
   
}
