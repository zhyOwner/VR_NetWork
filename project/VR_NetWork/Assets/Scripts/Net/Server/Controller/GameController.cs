using Common;
using UnityEngine;

namespace Servers{
    class GameController : BaseController
    {
        public GameController()
        {
        }

        public string Move(string data, Client client, Server server){
            /* ͬ���������˿ڵ���ӦΪ MoveResponse */
            server.BroadcastMessage(client , ActionCode.MoveResponse , data);
            return null;
        }
        
    }

}
