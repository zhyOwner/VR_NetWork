using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using VRTK;

public class InitializeEditor : Editor
{
    [MenuItem("VR/Initialized")]
    public static void Init()
    {
        
    }

    private static GameObject CreatePrefabAtPath(string path)
    {
        GameObject gameObj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        return PrefabUtility.InstantiatePrefab(gameObj) as GameObject;
    }
}
