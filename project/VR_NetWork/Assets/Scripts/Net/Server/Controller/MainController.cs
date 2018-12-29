using Common;
using UnityEngine;

namespace Servers{
    class MainController : BaseController
    {
        public MainController()
        {

        }

        public string Task(string data, Client client, Server server){
            server.BroadcastMessage(client , ActionCode.Task , data);
            return data;
        }
        
    }

}
