using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI coinDisplay, warning;
    [SerializeField] private Slider staminaDisplay;

    [SerializeField] private GameObject player, monster;
    private PlayerMovement playerMovement;

    [Header("Warning Effect")]

    [SerializeField] private float maxDist;
    [SerializeField] private float minDist;
    [SerializeField] private Image[] panels;

    void Start()
    {
        warning.gameObject.SetActive(false);
        playerMovement = player.GetComponent<PlayerMovement>();
        staminaDisplay.maxValue = playerMovement.PlayerMaxStamina;

    }

    void Update()
    {
        staminaDisplay.value = playerMovement.PlayerStamina;

        float dist = Vector3.Distance(player.transform.position, monster.transform.position);
        float proximity = Mathf.InverseLerp(maxDist, minDist, dist);

        Color targetColor = new Color(1f, 0f, 0f, proximity);

        for (int i = 0; i < panels.Length; i++)
        {

            panels[i].color = targetColor;
        }

    }

    public void CoinUpdate()
    {
        coinDisplay.text = "Coins left: " + CoinManager.instance.coinCount;
    }

    public void Warning()
    {
        if (warning.gameObject.activeSelf == false)
        warning.gameObject.SetActive(true);
    }
    

}
