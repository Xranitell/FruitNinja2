using System;
using UnityEngine;

public class PhysicalObject : MonoBehaviour
{
    public float mass = 1;
    
    private float _timer = 0;
    private Vector3 _nextPoint;
    
    public Vector3 Velocity { get; private set; }
    public bool UseGravity { get; set; }= true;
    
    public void AddForce(Vector3 direction, float force)
    {
        _timer = 0;
        Velocity = direction.normalized * force;
    }
    public void AddForce(Vector3 direction, float force, Vector3 impulse)
    {
        _timer = 0;
        Velocity = impulse * DataHolder.Config.impulseMultiplier;
        Velocity += (direction.normalized) * force;
    }

    private void FixedUpdate()
    {
        _nextPoint = GetNextPoint();
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _nextPoint, Time.deltaTime);
    }

    private Vector3 GetNextPoint()
    {
        var gravityForce = UseGravity? new Vector3(0, -(DataHolder.Config.gravity * 0.01f * mass), 0):Vector3.zero;
        _timer += Time.fixedDeltaTime;
        var newPos = transform.position + (Velocity + gravityForce * Mathf.Pow(_timer, 2)) ;
        Velocity += (gravityForce * _timer);
        return newPos;
    }
}