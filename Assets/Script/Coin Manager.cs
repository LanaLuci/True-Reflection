using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CoinManager : MonoBehaviour
{
    [Header("Coin")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int coinNum;
    public int coinCount {  get; private set; }
    public UnityEvent onCollected, nextPhase;

    [Header("Powerup")]
    private bool powerReady = false;
    [SerializeField] private float powerCD;
    private float powerTimer;
    [SerializeField] private GameObject powerPrefab;
    public UnityEvent onStunCollected;

    public static CoinManager instance;
    private float minX, maxX, minZ, maxZ, sizeMulti;



    IEnumerator Start() 
    { 
        instance = this;
        coinCount = coinNum;
        powerTimer = powerCD;
        for (int i = 0; i < 3; i++)
        {
            yield return null;
        }
        onCollected?.Invoke();
    }  

    void Update()
    {
        if (!powerReady)
        {
            if (powerTimer > 0)
            {
                powerTimer -= Time.deltaTime;
            }
            else
            {
                SpawnPower();
                powerReady = true;

            }
        }
    }

    public void ChangeCount(int amount)
    {
        coinCount += amount;
    }

    public void CheckCoin()
    {
        if (coinCount <= 0)
        {
            nextPhase.Invoke();
        }
    }
    public void ReadyNextPower()
    {
        powerTimer = powerCD;
        powerReady = false;

    }

    private void SpawnPower() 
    {
        for (int j = 0; j < 25; j++)
        {
            Vector3 pos = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
            if (!Physics.CheckSphere(pos, 0.5f))
            {

                Instantiate(powerPrefab, pos, Quaternion.identity);
                break;
            }

        }

    }
    public void SpawnCoin(int startX, int startZ, int width, int depth, float sizeMultiplier)
    {
        minX = startX;
        maxX = startX + (width - 1) * sizeMultiplier;
        minZ = startZ;
        maxZ = startZ + (depth - 1) * sizeMultiplier;
        sizeMulti = sizeMultiplier;

        for (int i = 0; i < coinCount; i++)
        {


            for (int j = 0; j < 25; j++)
            {
                Vector3 pos = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
                if (!Physics.CheckSphere(pos, 0.5f))
                {

                    Instantiate(coinPrefab, pos, Quaternion.identity);
                    break;
                }

            }

        }
    }
}
