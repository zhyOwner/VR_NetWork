
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
namespace Servers{
    public class ControllerManager {
        private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();
        private Server server;

        public ControllerManager(Server server) {
            this.server = server;
            InitController();
        }

        void InitController()
        {
            controllerDict.Add(RequestCode.Player, new PlayerController());
            controllerDict.Add(RequestCode.Main, new MainController());
            controllerDict.Add(RequestCode.Game, new GameController());
        }

        public void HandleRequest(RequestCode requestCode,ActionCode actionCode,string data,Client client)
        {
            BaseController controller;
            bool isGet = controllerDict.TryGetValue(requestCode, out controller);
            if (isGet == false)
            {
                Debug.Log("无法得到[" + requestCode + "]所对应的Controller,无法处理请求");return;
            }
            string methodName = Enum.GetName(typeof(ActionCode), actionCode);
            MethodInfo mi = controller.GetType().GetMethod(methodName);
            if (mi == null)
            {
                Debug.Log("[警告]在Controller["+controller.GetType()+"]中没有对应的处理方法:["+methodName+"]");return;
            }
            object[] parameters = new object[] { data,client,server };
            object o = mi.Invoke(controller, parameters);
            if(o==null||string.IsNullOrEmpty( o as string))
            {
                return;
            }
            server.SendResponse(client, actionCode, o as string);
        }

    }
}