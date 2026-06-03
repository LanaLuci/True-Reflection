using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float updateInterval, range;
    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        timer = 0f;
    }

    void Update()
    {
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
