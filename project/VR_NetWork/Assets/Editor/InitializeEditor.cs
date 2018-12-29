using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using VRTK;

public class InitializeEditor
{
    [MenuItem("GameObject/VR/Initialized")]
    public static void Init()
    {
        new GameObject("----------------------------------------------------------------");

        /* 创建 SceneManager */
        GameObject sceneManager = new GameObject("SceneManager");
        //InitializePort port = sceneManager.AddComponent<InitializePort>();

        /* 创建VRTK  */
        GameObject VRSdk = CreatePrefabAtPath("VR/Manager/[VRTK_SDKManager]");

        //VRSdk.name = "[VRManager]";
        //VRSdk.SetActive(false);

        GameObject VRScripts = CreatePrefabAtPath("VR/Manager/[VRTK_Scripts]");

        //VRScripts.name = "[VRScripts]";
        //VRScripts.SetActive(false);

        //VRTK_SDKManager manager = VRSdk.GetComponent<VRTK_SDKManager>();
        //manager.scriptAliasLeftController = VRScripts.transform.GetChild(1).gameObject;
        //manager.scriptAliasRightController = VRScripts.transform.GetChild(2).gameObject;

        /* 创建 FollowUI */
        GameObject followUI = CreatePrefabAtPath("VR/Manager/HeadsetFollower");
        //followUI.name = "HeadsetFollower";
        //followUI.SetActive(false);

        //GameObject camera = new GameObject("MainCamera");
        //camera.AddComponent<Camera>();
        //camera.SetActive(false);

        /* 赋值操作 */
        //port.VRTK_Sdkmanager = VRSdk;
        //port.VRTK_Scripts = VRScripts;
        //port.VRTK_LeftHand = VRSdk.transform.Find("SDKSetups/SteamVR/[CameraRig]/Controller (left)").gameObject;
        //port.VRTK_RightHand = VRSdk.transform.Find("SDKSetups/SteamVR/[CameraRig]/Controller (right)").gameObject;
        //port.VRTK_HeadsetFollower = followUI;
        //port.tittle = followUI.transform.Find("VRMenu/GuideCanvas/TittleText").GetComponent<Text>();
        //port.taskContent = followUI.transform.Find("VRMenu/GuideCanvas/ContentText").GetComponent<Text>();
        //port.VRTK_GuidePanel = followUI.transform.Find("VRMenu/GuideCanvas").gameObject;
        //port.PC_Camera = camera;

        new GameObject("----------------------------------------------------------------");
    }

    private static GameObject CreatePrefabAtPath(string path)
    {
        GameObject gameObj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        return PrefabUtility.InstantiatePrefab(gameObj) as GameObject;
    }
}
