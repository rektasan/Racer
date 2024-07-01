using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarBody : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        transform.rotation = target.rotation;
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, 0.2f);
    }
}