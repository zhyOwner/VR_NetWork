using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Common;
using UnityEngine;

namespace Servers{
    public class Server {
        private IPEndPoint _ipEndPoint;
        private Socket _serverSocket;
        private List<Client> _clientArray = new List<Client>();//所有连接的客户端

        public Dictionary<string , Client> nameClientDic = new Dictionary<string, Client>(); 
        private ControllerManager _controllerManager;

        public Server(){}
        public Server(string ipStr , int port){
            _controllerManager = new ControllerManager(this);
            _ipEndPoint = new IPEndPoint(IPAddress.Parse(ipStr) , port);
        }

        public void Start(){
            _serverSocket = new Socket(AddressFamily.InterNetwork , SocketType.Stream , ProtocolType.Tcp);
            _serverSocket.Bind(_ipEndPoint);
            _serverSocket.Listen(0);
            _serverSocket.BeginAccept(AcceptCallBack , null);
            Debug.Log("服务端启动成功...");
        }

        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket clientSocket = _serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket , this);
            client.Start();
            _clientArray.Add(client);
            _serverSocket.BeginAccept(AcceptCallBack , null);
            Debug.Log("客户端连接");
        }

        public void SendResponse(Client client , ActionCode actionCode,string data){
            client.Send(actionCode, data);
        }

        public void BroadcastMessage(Client client,ActionCode actionCode,string data){
            foreach (Client _client in nameClientDic.Values)
            {
                if(_client != client){
                    SendResponse(_client , actionCode , data);
                }
            }
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            _controllerManager.HandleRequest(requestCode, actionCode, data, client);
        }
        public void RemoveClient(Client client){
            lock(_clientArray){
                _clientArray.Remove(client);
            }

            lock(nameClientDic){
                string userName = "";
                foreach (KeyValuePair<string , Client> item in nameClientDic)
                {
                    if(client == item.Value){
                        userName = item.Key;
                        break;
                    }
                }
                if(userName == "") return;
                nameClientDic.Remove(userName);
                PlayerManager.Instance.RemovePlayer(userName.Split('|')[0]);
            }
        }

        public bool AddClient(string userName , Client client){ //555|Monitor
            lock (nameClientDic)
            {
                if (nameClientDic.ContainsKey(userName)) return false;
                string[] player = userName.Split('|');
                if (!PlayerManager.Instance.AddPlayer(player[0], new Player(player[0], player[1]))) return false;
                nameClientDic.Add(userName, client);
                return true;
            }
        }
    }

}
