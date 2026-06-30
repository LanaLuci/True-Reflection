using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private float spd, spin_spd;
    private bool floating;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0, spin_spd, 0);
        if (floating)
        {
            rb.linearVelocity = transform.up * spd;
            if (transform.position.y >= 3f)
            {
                floating = false;
            }
        }
        else
        {
            rb.linearVelocity = transform.up * spd * (-1);
            if (transform.position.y <= 1f)
            {
                floating = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            OnPicked();

        }
    }

    protected abstract void OnPicked();
}
