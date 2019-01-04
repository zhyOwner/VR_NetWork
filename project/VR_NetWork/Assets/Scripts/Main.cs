using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;
    public static bool isTest = false;

    /* 管理所有的需要同步的角色 */
    public static Dictionary<string, GameObject> _players = new Dictionary<string, GameObject>();

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        RequestManager.Instance.Tick();
    }



    public static GameObject GetPlayer(string id)
    {
        return _players.ContainsKey(id) ? _players[id] : null;
    }
}
