using Common;
using UnityEngine;

namespace Servers{
    class GameController : BaseController
    {
        public GameController()
        {
        }

        public string Move(string data, Client client, Server server){
            /* 同步给其他端口的响应为 MoveResponse */
            server.BroadcastMessage(client , ActionCode.MoveResponse , data);
            return null;
        }
        
    }

}
