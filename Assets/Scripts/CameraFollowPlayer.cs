using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    /// <summary>
    /// -23.61,     0.28    50.663
    /// -23.5   1.2     49
    /// </summary>
    /// 

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 cameraOffset;


    private void Start()
    {
        cameraOffset = target.position - transform.position;
        transform.position = target.position - cameraOffset;
    }

    private void LateUpdate()
    {
        this.transform.position = target.position - cameraOffset;
    }
}
