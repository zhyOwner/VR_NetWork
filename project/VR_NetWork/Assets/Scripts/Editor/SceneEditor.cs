using UnityEngine;
using UnityEditor;
using VRTK;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneEditor : EditorWindow
{

    [MenuItem("VR/Show VR Editor Window" , false , 1)]
    public static void ShowWindow()
    {
        GetWindow(typeof(SceneEditor));
    }

    //private void OnGUI()
    //{
    //    GUILayout.BeginVertical();
    //    /* SCENE TITLE */
    //    GUILayout.Space(15);
    //    GUIStyle style = new GUIStyle();
    //    style.fontSize = 18;
    //    style.alignment = TextAnchor.UpperCenter;
    //    GUILayout.Label(SceneManager.GetActiveScene().name , style);
    //    /* END SCENE TITLE */
    //    GUILayout.Space(15);
    //    /* START INIT RESOURCES */
    //    GUIStyle init = new GUIStyle();
    //    init.fontSize = 14;
    //    init.padding = new RectOffset(15,0,0,0);
    //    init.alignment = TextAnchor.UpperLeft;
    //    GUILayout.Label("可初始化的资源", init);

        

    //    GUILayout.BeginHorizontal();
    //    GUIStyle one = new GUIStyle();
    //    one.fontSize = 12;
    //    one.padding = new RectOffset(20, 0, 10, 0);
    //    one.alignment = TextAnchor.UpperLeft;
    //    GUILayout.Label("naasdfasdfasdfasdme", one);

    //    GUILayout.EndHorizontal();
    //    /* END INIT RESOURCES */







    //    GUILayout.EndVertical();
    //}





    [MenuItem("VR/Initialized")]
    public static void Init()
    {
        /* 开始创建对象 */
        GameObject start = new GameObject(".............................................................");
        start.transform.SetSiblingIndex(10);

        /* 1. 创建 VRTKManager */
        GameObject manager = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("VR/Manager/[VRTK_SDKManager]"));
        VRTK_SDKManager VRSDK_Manager = manager.GetComponent<VRTK_SDKManager>();
        manager.transform.SetSiblingIndex(11);

        /* 2. 创建 VRTKScripts */
        GameObject VRSDK_Scripts = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("VR/Manager/[VRTK_Scripts]"));

        /* 3. 初始化脚本的引用 */
        GameObject leftHand = VRSDK_Scripts.transform.Find("LeftController").gameObject;
        GameObject rightHand = VRSDK_Scripts.transform.Find("RightController").gameObject;
        VRSDK_Manager.scriptAliasLeftController = leftHand;
        VRSDK_Manager.scriptAliasRightController = rightHand;
        VRSDK_Scripts.transform.SetSiblingIndex(12);

        /* 4. 创建 HeadSetFollow */
        GameObject headsetFollow = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("VR/Manager/HeadsetFollower"));
        headsetFollow.transform.SetSiblingIndex(13);
        /* 创建监控端的摄像机 */
        GameObject camera = GameObject.Find("Main Camera");
        if (camera == null)
        {
            camera = new GameObject("Main Camera");
            camera.AddComponent<Camera>();
        }
        camera.transform.SetSiblingIndex(14);

        /* 创建 场景管理对象及添加管理脚本 */
        GameObject sceneManager = new GameObject("SceneManager");
        InitializePort initializePort = sceneManager.AddComponent<InitializePort>();
        sceneManager.transform.SetSiblingIndex(15);
        initializePort.VRTK_Sdkmanager = manager;
        initializePort.VRTK_Scripts = VRSDK_Scripts;
        initializePort.VRTK_LeftHand = leftHand;
        initializePort.VRTK_RightHand = rightHand;
        initializePort.VRTK_HeadsetFollower = headsetFollow;

        initializePort.PC_Camera = camera;
        initializePort.tittle = headsetFollow.transform.Find("VRMenu/GuideCanvas/TittleText").GetComponent<Text>();
        initializePort.taskContent = headsetFollow.transform.Find("VRMenu/GuideCanvas/ContentText").GetComponent<Text>();
        /* 结束创建对象 */
        GameObject end = new GameObject(".............................................................");
        end.transform.SetSiblingIndex(100);

        /* 关掉所有操作的对象 */
        manager.gameObject.SetActive(false);
        VRSDK_Scripts.gameObject.SetActive(false);
        headsetFollow.gameObject.SetActive(false);
        camera.gameObject.SetActive(false);

        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        EditorSceneManager.SaveOpenScenes();
    }
}
