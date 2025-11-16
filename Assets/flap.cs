using UnityEngine;
using UnityEngine.InputSystem;

public class flap : MonoBehaviour
{
    public Rigidbody2D myRigidBody2d;
    public int flightSpeed = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) == true)
        {
            myRigidBody2d.linearVelocity = Vector2.up * flightSpeed;
        }
        
    }
}
