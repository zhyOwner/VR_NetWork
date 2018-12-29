using Servers;
using UnityEngine;

public class ServerManager : MonoBehaviour {
    public static ServerManager instance;
    public Server server;
    private void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Run(string ip) {
        server = new Server(ip, 6688);
        server.Start();
    }
}