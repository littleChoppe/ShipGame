using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {

    public float existTime;
	void Start () {
        Destroy(gameObject, existTime);
	}
	
}
