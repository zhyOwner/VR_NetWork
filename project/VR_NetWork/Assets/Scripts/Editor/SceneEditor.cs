using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using VRTK;
using UnityEditor.SceneManagement;

public class SceneEditor : EditorWindow
{

    [MenuItem("VR/Show VR Editor Window" , false , 1)]
    public static void ShowWindow()
    {
        GetWindow(typeof(SceneEditor));
    }


    [MenuItem("VR/Initialized")]
    public static void Init()
    {
        /* 1. 创建 VRTKManager */

        /* 2. 创建 VRTKScripts */

        /* 3. 初始化脚本的引用 */
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        /* TITTLE START */
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 16;
        GUI.skin.label.alignment = TextAnchor.UpperCenter;
        GUILayout.Label("VR 场景编辑窗口");
        /* TITTLE END */

        /* Current Scene START */
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 14;
        GUI.skin.label.alignment = TextAnchor.UpperLeft;
        GUILayout.Label("Currently Scene:" + EditorSceneManager.GetActiveScene().name);
        GUILayout.Label("Time:" + System.DateTime.Now);

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();



        bool isManager = GUILayout.Toggle(false , "VRTK_Manager");
        bool isScripts = GUILayout.Toggle(false, "VRTK_Scripts");
        bool isHeadSet = GUILayout.Toggle(false, "VRTK_HeadFollow");
     
       
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);
        if (GUILayout.Button("Init"))
        {
            if (isManager)
            {
                /* 创建 VRTK_SDKManager */
            }

            if (isScripts)
            {
                /* 创建 VRTK_SDKManager */
            }

            if (isHeadSet)
            {

            }

        }
        EditorGUILayout.EndVertical();
    }



   
}
