using UnityEngine;

public class ObjectMoveRequest : MonoBehaviour {
    
    [HideInInspector]public float rate = 30;//同步发送位置的速率
    private void Awake() {
        _requestManager = Main.Instance.gameObject.GetComponent<ObjectMoveRequestManager>();
        _name = transform.name;
        
        /* 测试阶段不需要同步 */
        //if(Main.isTest) return;

        /* 延迟两秒发送 */
        if(ClientManager.Port == Port.Operator)
            InvokeRepeating("SyncObject" , 2 , 1/rate);
    }
    private void OnEnable() {
        Debug.Log(_requestManager);
        _requestManager.AddMoveRequest(_name, this);

        /* 如果不是操作端则将 刚体移除 */
        if (ClientManager.Port != Port.Operator)
        {
            if (GetComponent<Rigidbody>())
                DestroyImmediate(GetComponent<Rigidbody>());
        }
    }
    private void OnDisable() {
        _requestManager.RemoveRequest(_name);
    }

    private void SyncObject()
    {
        //本地 Object 需要同步的数据 name Position Rotation
        _requestManager.SendRequest(_name+ "|" + transform.position.x+"," + transform.position.y + "," +
                    transform.position.z + "|" + transform.eulerAngles.x + "," + transform.eulerAngles.y +
                    "," + transform.eulerAngles.z );
    }


    /// <summary>
    ///响应 Transform 信息
    /// </summary>
    /// <param name="msg"></param>
    public void OnResponse(string msg){
        string[] transformInfo = msg.Split('|');
        transform.position = Parse(transformInfo[1]);
        transform.eulerAngles = Parse(transformInfo[2]);
    }


    private Vector3 Parse(string msg)
    {
        string[] str = msg.Split(',');
        float x = float.Parse(str[0]);
        float y = float.Parse(str[1]);
        float z = float.Parse(str[2]);
        return new Vector3(x, y , z);
    }

    private ObjectMoveRequestManager _requestManager;
    private string _name;
}