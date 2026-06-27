using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float spd, sprint_spd, jumpForce, maxStamina;
    [SerializeField] private float stamina;
    public float PlayerStamina => stamina;
    public float PlayerMaxStamina => maxStamina;
    [SerializeField] private bool sprintable;

    [SerializeField] private LayerMask floorLayer;
    private bool onFloor;
    private bool doublejump;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stamina = maxStamina;

    }


    void FixedUpdate()
    {


        onFloor = Physics.RaycastAll(transform.position, Vector3.down, 1.2f, floorLayer).Length > 0;
        sprintable = stamina > 0;

        if (onFloor)
        {
            doublejump = true;
        }
        if (PauseManager.gamePaused)
        {
            return;
        }
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
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftControl) && sprintable)
        {
            rb.linearVelocity = new Vector3(move.x * sprint_spd, rb.linearVelocity.y, move.z * sprint_spd);
            if (stamina > 0)
            stamina -= Time.deltaTime * 6f;
        }
        else
        {
            rb.linearVelocity = new Vector3(move.x * spd, rb.linearVelocity.y, move.z * spd);
            if (stamina < maxStamina)
            stamina += Time.deltaTime;

        }



    }


}
