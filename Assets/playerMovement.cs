using UnityEngine;
using UnityEngine.InputSystem;
public class playerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D player;
    public float speed;
    public float dSpeed;
    public InputAction movementControls;
    public InputAction actions; 
    Vector3 mousePosition = Input.mousePosition;
    Vector2 moveDirection = Vector2.zero;
    
    private void OnEnable()
    {
        movementControls.Enable();
        actions.Enable();
    }
    private void OnDisable()
    {
        movementControls.Disable();
        actions.Disable();
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = movementControls.ReadValue<Vector2>();
        //actions.
        
    }
    private void FixedUpdate()
    {
        player.linearVelocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        //player.linearVelocity = moveDirection * speed; 
        //point to mouse position
        //if ()
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); 
        Vector2 direction = (mousePos - transform.position); 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Dash(Vector2 direction)
    {
        //player.linearVelocity = 
    }
}
