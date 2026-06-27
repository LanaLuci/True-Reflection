using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float updateInterval, range;
    public UnityEvent onPlayerCaught;
    private NavMeshAgent agent;
    private float timer;
    public static bool playerCaught {  get; private set; }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        timer = 0f;
        playerCaught = false;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target.position) < 3f)
        {
            onPlayerCaught.Invoke();
            playerCaught=true;
        }
        if (playerCaught)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("SampleScene");
            }
            return;
        }
        if (PauseManager.gamePaused)
            return;


        timer += Time.deltaTime;
        if (timer > updateInterval)
        {
            timer = 0f;
            StartChase();
        }

        Vector3 velocity = agent.velocity;

        if (velocity.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);
            targetRotation *= Quaternion.Euler(0f, -90f, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            
        }
    }



    private void StartChase()
    {
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist <= range && agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
        }
    }

    public void SpeedChange(float amount)
    {
        agent.speed += amount;
    }
}
