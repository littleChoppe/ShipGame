using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

	void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
