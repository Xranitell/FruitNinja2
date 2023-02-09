using System;
using UnityEngine;

public class PhysicalObject : MonoBehaviour
{
    public float mass = 1;
    public float gravity = 9.8f;
    public Vector3 velocity;
    public bool useGravity = true;
    public static float SpeedScaler = 1f;
    private Vector3 impulse;
    
    private float _timer = 0;

    private Vector3 nextPoint;

    public void AddForce(Vector3 direction, float force)
    {
        _timer = 0;
        velocity = direction.normalized * force;
    }
    public void AddForce(Vector3 direction, float force, Vector3 impulse)
    {
        _timer = 0;
        velocity = impulse;
        
        velocity += (direction.normalized) * force;
    }

    private void FixedUpdate()
    {
        nextPoint = GetNextPoint();
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, nextPoint, Time.deltaTime);
    }

    private Vector3 GetNextPoint()
    {
        var gravityForce = useGravity? new Vector3(0, -(gravity * 0.01f * mass), 0):Vector3.zero;
        _timer += Time.fixedDeltaTime;
        var newPos = transform.position + (velocity + gravityForce * Mathf.Pow(_timer, 2) * SpeedScaler) ;
        velocity += (gravityForce * _timer);
        return newPos;
    }
}