using Common;
using UnityEngine;

namespace Servers{
    class PlayerController : BaseController
     {  
        public PlayerController()
        {
            requestCode = RequestCode.Player;
        }

         /// <summary>
         /// 客户端登录
         /// </summary>
         /// <param name="userName">id|Port</param>
         /// <param name="client"></param>
         /// <param name="server"></param>
         /// <returns></returns>
         public string Login(string userName , Client client , Server server){
            if(!server.nameClientDic.ContainsKey(userName))
            {
                /*
                 * 1.首先判断是否存在当前端口
                 * 2.是否存在当前id
                 */
                if (!server.AddClient(userName, client))
                {
                    return ((int)ReturnCode.Fail).ToString() + ",登录失败 请查看 id 或 端口是否重复";
                }
                //TODO 通知上线消息 加上id  消息内容为 id|Port
                server.BroadcastMessage(client , ActionCode.GetPlayer, userName);/* 通知其他端口，我上线了  */
                return ((int)ReturnCode.Success).ToString() + ",登录成功";
            }
            else{
                return ((int)ReturnCode.Fail).ToString() + ",登录失败 请查看 id 或 端口是否重复";
            }
        }

         /// <summary>
         /// 客户端登出
         /// </summary>
         /// <param name="id"></param>
         /// <param name="client"></param>
         /// <param name="server"></param>
         /// <returns></returns>
         public string Logout(string userName, Client client, Server server)
         {
             Debug.Log("服务器收到客户端下线消息：" + userName);
             //TODO 通知下线消息加上 id  消息内容为 id|Port
             server.BroadcastMessage(client, ActionCode.Logout, userName);/* 通知其他端口，我下线了 */
             server.RemoveClient(client);
             PlayerManager.Instance.RemovePlayer(userName.Split('|')[0]);
             return null;
         }

        /// <summary>
        /// 客户端获取所有连接的端口信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="client"></param>
        /// <param name="server"></param>
        /// <returns>所有连接的客户端的信息</returns>
         public string GetPlayer(string data, Client client, Server server){
            string players = PlayerManager.Instance.GetPlayer();
            if(players != ""){
                return ((int)ReturnCode.Success).ToString()+ players;
            }
            else{
                return ((int)ReturnCode.Fail).ToString()+"没有其他端口登录";
            }
        }
    }
}

