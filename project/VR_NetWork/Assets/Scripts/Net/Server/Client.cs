
using System;
using System.Net.Sockets;
using Common;
using UnityEngine;

namespace Servers{
    public class Client {
        private Socket clientSocket;
        private Server server;
        private Message msg = new Message();

        public Client() { }
        public Client(Socket clientSocket,Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
        }

        public void Start()
        {
            if (clientSocket == null || clientSocket.Connected == false) return;
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                if (clientSocket == null || clientSocket.Connected == false) return;
                int count = clientSocket.EndReceive(ar);
                if (count == 0)
                {
                   Close();
                }
                msg.ReadMessage(count,OnProcessMessage);
                Start();
            }
            catch (Exception e)
            {
                Close();
            }
        }

        private void OnProcessMessage(RequestCode requestCode,ActionCode actionCode,string data){
            server.HandleRequest(requestCode, actionCode, data, this);
        }

        public void Send(ActionCode actionCode, string data){
            try
            {
                byte[] bytes = Message.PackData(actionCode, data);
                clientSocket.Send(bytes);
            }
            catch(Exception e)
            {
                Debug.Log("无法发送消息。。。");
            }
        }

        public bool IsConnected(){
            return clientSocket.Connected;
        }
        private void Close(){
            if(clientSocket != null){
                clientSocket.Close();
            }
            server.RemoveClient(this);
        }
       
    }
}
