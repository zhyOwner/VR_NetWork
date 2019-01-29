using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
    1.场景中 HTC 内容的管理
    2.非 HTC 内容管理
 */
public class InitializePort : MonoBehaviour
{

    # region HTC-VIVE
    [Header("HTC-VIVE")]
    public GameObject VRTK_Sdkmanager;
    public GameObject VRTK_Scripts;
    public GameObject VRTK_HeadsetFollower;
    public GameObject VRTK_LeftHand;
    public GameObject VRTK_RightHand;
    public Text tittle;
    public Text taskContent;
    # endregion
    #region PC
    [Header("PC")]
    public GameObject PC_Camera;
    #endregion

    public static InitializePort instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if(ClientManager.Port != Port.Monitor )
        {
            HTCInit();
        }
        else
        {
            PCInit();
        }
    }
   
    //根据端口 初始化 HTC 或者 PC 模式
    public void HTCInit()
    {
        VRTK_Sdkmanager.SetActive(true);
        VRTK_Scripts.SetActive(true);
        VRTK_LeftHand.SetActive(true);
        VRTK_RightHand.SetActive(true);
        VRTK_HeadsetFollower.SetActive(true);
    }

    public void PCInit()
    {
        PC_Camera.SetActive(true);
    }


}