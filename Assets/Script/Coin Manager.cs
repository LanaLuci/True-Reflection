using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int coinNum;
    public int coinCount {  get; private set; }
    public UnityEvent onCollected, nextPhase;
    public static CoinManager instance;

    IEnumerator Start() 
    { 
        instance = this;
        coinCount = coinNum;
        for (int i = 0; i < 3; i++)
        {
            yield return null;
        }
        onCollected?.Invoke();
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
    public void SpawnCoin(int startX, int startZ, int width, int depth, float sizeMultiplier)
    {
        float minX = startX;
        float maxX = startX + (width - 1) * sizeMultiplier;
        float minZ = startZ;
        float maxZ = startZ + (depth - 1) * sizeMultiplier;

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
