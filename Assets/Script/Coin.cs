using UnityEngine;


public class Coin : Pickup
{
    protected override void OnPicked()
    {
        CoinManager.instance.ChangeCount(-1);
        CoinManager.instance.onCollected.Invoke();
        Destroy(this.gameObject);
    }

}
