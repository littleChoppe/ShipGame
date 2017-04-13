using UnityEngine;
using System.Collections;

public class FollowThePlayer : MonoBehaviour {

    public float Speed = 1f;
    private Vector3 offset = Vector3.zero;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(player);
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Speed * Time.deltaTime);
        Quaternion targetRotation = Quaternion.LookRotation(player.position - targetPosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Speed * Time.deltaTime);
    }
}
