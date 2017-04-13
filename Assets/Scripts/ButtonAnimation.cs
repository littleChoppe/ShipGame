using UnityEngine;
using System.Collections;
using DG.Tweening;
public class ButtonAnimation : MonoBehaviour {

    private Tweener tween;
    void Start()
    {
        Debug.Log("动画");
         tween = transform.DOScale(new Vector3(1.2f, 1.2f, 0f), 1f);
    }
    void OnMouseDown()
    {
        Debug.Log("鼠标在上");
        tween.PlayForward();
    }
}
