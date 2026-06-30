using UnityEngine;
using System.Collections;

public class StunOrb : Pickup
{
    [SerializeField] private float stun_dur;
    protected override void OnPicked()
    {
        Monster monster = GameObject.Find("Monster").GetComponent<Monster>();
        if (monster == null)
            Debug.LogWarning("cant find");
        monster.Stun(stun_dur);
        CoinManager.instance.ReadyNextPower();
        Destroy(gameObject);
    }
}
