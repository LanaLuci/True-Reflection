using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float spd, jumpForce;

    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private bool onFloor;
    [SerializeField] private bool doublejump;


    [SerializeField] private float currentHP;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {

        onFloor = Physics.RaycastAll(transform.position, Vector3.down, 1.2f, floorLayer).Length > 0;

        if (onFloor)
        {
            doublejump = true;
        }

        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");


        rb.linearVelocity = new Vector3(move.x * spd, rb.linearVelocity.y, move.z * spd);


        if (Input.GetButtonDown("Jump"))
        {
            if (onFloor)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                onFloor = false;

            }
            else if (doublejump)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                doublejump = false;
            }
        }
    }


}
