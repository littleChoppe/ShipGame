using UnityEngine;
using System.Collections;

public class Camera3DAdapter : MonoBehaviour {

    private float designHeight = 602;
    private float designWidth = 1024;
    private float rate1, rate2;
	void Start () {
        rate1 = designHeight / designWidth;
        rate2 = Screen.height / (float)Screen.width;
        gameObject.GetComponent<Camera>().fieldOfView *= 1 + (rate2 - rate1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
