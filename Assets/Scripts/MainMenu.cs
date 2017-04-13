using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

    private Button[] btns;
	void Start () 
    {
        btns = FindObjectsOfType<Button>();
        foreach(Button btn in btns)
        {
            OnClick(btn);
        }
	}

    void OnClick(Button btn)
    {
        switch(btn.name)
        {
            case "PlayButton": btn.onClick.AddListener(delegate { this.OnPlayButton(); });  break;
            case "SettingButton": btn.onClick.AddListener(delegate { this.OnSettingButton(); }); break;
            case "ExitButton": btn.onClick.AddListener(delegate { this.OnExitButton(); }); break;
            default: Debug.Log("无此按钮"); break;
        }

        //EventTriggerListener.Get(btn.gameObject).OnPress = OnPressAnimation;
        EventTriggerListener.Get(btn.gameObject).OnEnter = OnEnterAnimation;
        EventTriggerListener.Get(btn.gameObject).OnExit = OnExitAnimation;
    }
	

    void OnPressAnimation(GameObject go)
    {
        go.transform.DOScale(new Vector3(1f, 1f, 0f), 0.5f).PlayForward();

    }

    void OnEnterAnimation(GameObject go)
    {
        go.transform.DOScale(new Vector3(1.2f, 1.2f, 0f), 0.5f).PlayForward();
    }

    void OnExitAnimation(GameObject go)
    {
        go.transform.DOScale(new Vector3(1f, 1f, 0f), 0.5f).PlayForward();
    }
    void OnPlayButton()
    {
        Debug.Log("开始游戏");
        SceneManager.LoadScene("Scenes/02_World");
    }

    void OnSettingButton()
    {
        Debug.Log("打开游戏设置界面");
    }

    void OnExitButton()
    {
        Debug.Log("退出游戏");
        Application.Quit();
    }
}
