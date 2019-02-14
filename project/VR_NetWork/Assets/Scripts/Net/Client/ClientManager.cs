using System;
using System.Net.Sockets;
using Common;
using UnityEngine;

public class ClientManager : MonoBehaviour {

    public static ClientManager Instance;
    private int PORT = 6688;

    private Socket _clientSocket;

    private string id;
    public  static Port Port;

    private Message msg = new Message();

    public string ID{
        get{
            return id;
        }
        set{
            id = value;
        }
    }

   

    private void Awake() {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnInit(string ip ,string id, Action<string> action)
    {
        this.ID = id;
        _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            _clientSocket.Connect(ip, PORT);
            OnStart();
            action("连接成功,正在登录请稍等...");
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            Remind.instance.Report("连接失败，请检查ip地址或用户名是否输入有误...");
        }
    }

    private void OnStart(){
        _clientSocket.BeginReceive(msg.Data,msg.StartIndex,msg.RemainSize, SocketFlags.None, ReceiveCallBack, null);
    }

    private void ReceiveCallBack(IAsyncResult ar){
        try
        {
            if (_clientSocket == null || _clientSocket.Connected == false) return;
            int count = _clientSocket.EndReceive(ar);

            msg.ClientReadMessages(count, OnProcessDataCallback);

            OnStart();
        }
        catch (System.Exception e )
        {
            Debug.Log(e.Message);
        }
    }

    private void OnProcessDataCallback(ActionCode actionCode, string data)
    {
        HandleResponse(actionCode , data);
    }

    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] bytes = Message.ClientPackData(requestCode, actionCode, data);
        _clientSocket.Send(bytes);
    }


    private void OnDestroy() {
        try
        {
            SendRequest(RequestCode.Player, ActionCode.Logout, id+"|"+Port);
            Debug.Log("客户端下线消息发送： " + id + "|" + Port);
            _clientSocket.Close();
        }
        catch(Exception e)
        {
            Debug.LogWarning("无法关闭跟服务器端的连接！！" + e);
        }
    }


    public void HandleResponse(ActionCode actionCode, string data){
        RequestManager.Instance.HandleReponse(actionCode , data);
    }
}