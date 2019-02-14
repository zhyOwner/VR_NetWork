using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Remind : MonoBehaviour
{
    public static Remind instance;
    public Text remindText;
    public Button sureButton;
    public GameObject panel;

    private Action _action;
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        sureButton.onClick.AddListener(OnSureButtonClick);
    }

    private void OnSureButtonClick()
    {
        _action?.Invoke();
        _action = null;
        panel.SetActive(false);
    }

    public void Report(string msg , Action action = null)
    {
        panel.SetActive(true);
        remindText.text = msg;
        this._action = action;
    }



   
}
