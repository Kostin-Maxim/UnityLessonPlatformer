using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float smoothTine = 0.2f;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTine);
    }
}
