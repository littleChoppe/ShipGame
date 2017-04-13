using UnityEngine;
using System.Collections;

public class HandleInput : MonoBehaviour {

    public float MoveSpeed = 5f;
	public void ControlMove(Vector2 dir)
    {
        Vector3 targetDir = new Vector3(dir.x, 0f, dir.y);
        Vector3 targetPoint = targetDir + transform.position;
        this.transform.LookAt(targetPoint);
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);

    }

    public void Attack()
    {
        Debug.Log("攻击");
    }
}
