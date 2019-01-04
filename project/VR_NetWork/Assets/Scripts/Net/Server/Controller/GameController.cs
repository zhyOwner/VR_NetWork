using Common;
using UnityEngine;

namespace Servers{
    class GameController : BaseController
    {
        public GameController()
        {
        }

        /// <summary>
        /// 响应客户端角色的请求
        /// </summary>
        /// <param name="data">请求的数据</param>
        /// <param name="client">哪个客户端的请求</param>
        /// <param name="server">服务器</param>
        /// <returns></returns>
        public string Move(string data, Client client, Server server){
            /* ͬ���������˿ڵ���ӦΪ MoveResponse */
            server.BroadcastMessage(client , ActionCode.MoveResponse , data);
            return null;
        }

        /// <summary>
        /// 响应客户端射线点击事件的请求
        /// </summary>
        /// <param name="data"></param>
        /// <param name="client"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        public string Pointer(string data, Client client, Server server){
            server.BroadcastMessage(client , ActionCode.MoveResponse , data);
            return null;
        }



        /// <summary>
        /// 响应客户端 Object Move 等请求
        /// </summary>
        /// <param name="data"></param>
        /// <param name="client"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        public string  ObjectMove(string data, Client client, Server server){
            server.BroadcastMessage(client , ActionCode.ObjectMove , data);
            return null;
        }
        
    }

}
