using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginRequest : BaseRequest
{
    public InputField ipInputField; 
    public InputField idInputField; 
    public Dropdown portDropdown;
    public Button sureButton;
    public Text successText;

    public override void Start()
    {
        requestCode = RequestCode.Player;
        actionCode = ActionCode.Login;
        base.Start();
        /* 从本地获取 记录 */
        ipInputField.text = PlayerPrefs.GetString("ip");
        idInputField.text = PlayerPrefs.GetString("id");
        portDropdown.value = PlayerPrefs.GetInt("port");
        /* 监听事件 */
        sureButton.onClick.AddListener(OnSureButtonClick);
    }

    private void OnSelectProt(int port)
    {
        if (port == 0)
        {
            try
            {
                CreateServer();//判断是否有监控端登录
                isLoginSuccess = true;
            }
            catch (Exception e)
            {
                isLoginSuccess = false;
                Remind.instance.Report("监控端已经登录，请更换端口..." , ()=>{
                    sureButton.interactable = true;
                    if(_server != null)
                        DestroyImmediate(_server.gameObject);
                });
            }
        }
        else
        {
            isLoginSuccess = true;
        }

        if (isLoginSuccess)
            CreatePort(port);
    }


    private void OnSureButtonClick()
    {
        sureButton.interactable = false;
        if (ipInputField.text == "") return;
        if(idInputField.text == "") return;
        OnSelectProt(portDropdown.value);
        if (isLoginSuccess)
            StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        try
        {
            SendRequest(idInputField.text + "|" + (Port)portDropdown.value);
        }
        catch (Exception e)
        {
            Remind.instance.Report("登录失败" , ()=>{
                sureButton.interactable = true;
            });
        }
        
    }

    /// <summary>
    /// 创建 Server
    /// </summary>
    private void CreateServer()
    {
        if(_server != null) DestroyImmediate(_server.gameObject);
        _server = new GameObject("server").AddComponent<ServerManager>();
        _server.Run(ipInputField.text);
    }

    /// <summary>
    /// 创建客户端网络管理脚本
    /// </summary>
    /// <param name="port"></param>
    private void CreatePort(int port)
    {
        if (_client == null)
        {
            _client = new GameObject("client").AddComponent<ClientManager>();
            _client.gameObject.AddComponent<LogoutRequest>();
        }
        _client.OnInit(ipInputField.text,idInputField.text , success => { successText.text = success; });
        ClientManager.Port = (Port) port;
    }

    /// <summary>
    /// 接受响应
    /// </summary>
    /// <param name="data"></param>
    public override void OnResponse(string data)
    {
        Debug.Log(data);
        string[] response = data.Split(',');
        ReturnCode code = (ReturnCode) int.Parse(response[0]);
        switch (code)
        {
            case ReturnCode.Success:
                SceneManager.LoadScene("MainScene");
                break;
            case ReturnCode.Fail:
                Remind.instance.Report("登录失败，请检查ip地址和用户名..." , () =>
                {
                    successText.text = "";
                    sureButton.interactable = true;
                });
                break;
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        PlayerPrefs.SetInt("port" , portDropdown.value);
        PlayerPrefs.SetString("ip" , ipInputField.text);
        PlayerPrefs.SetString("id", idInputField.text);
    }

    private ServerManager _server;
    private ClientManager _client;
    private bool isLoginSuccess = false;
}
