using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIController : MonoBehaviour
{
    [Header("Basics")]
    [SerializeField] private Slider staminaDisplay;
    [SerializeField] private TextMeshProUGUI coinDisplay, warning;


    [SerializeField] private GameObject player, monster;
    private PlayerMovement playerMovement;

    [Header("Warning Effect")]

    [SerializeField] private float maxDist;
    [SerializeField] private float minDist;
    [SerializeField] private Image[] panels;

    [Header("Flash Effect")]
    [SerializeField] private Image flashScreen;

    [Header("Game Over/Pause")]

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;

    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timeDisplay;
    private float timePlayed;

    void Start()
    {
        warning.gameObject.SetActive(false);
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        timePlayed = 0;
        playerMovement = player.GetComponent<PlayerMovement>();
        staminaDisplay.maxValue = playerMovement.PlayerMaxStamina;

    }

    void Update()
    {
        staminaDisplay.value = playerMovement.PlayerStamina;

        if (!PauseManager.gamePaused)
        {
            timePlayed += Time.deltaTime;
            int minute = Mathf.FloorToInt(timePlayed / 60);
            int second = Mathf.FloorToInt(timePlayed % 60);
            timeDisplay.text = minute + ":" + second;
        }

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
        {
            warning.gameObject.SetActive(true);
            timeDisplay.color = new Color(0.8f, 0f, 0f);
        }
    
    }

    public void GameOverInit()
    {
        if (!gameOverScreen.activeSelf)
        {
            StartCoroutine(GameOver());
            PlayerPrefs.SetFloat("TimePlayed", timePlayed);
        }
    }



    private IEnumerator GameOver()
    {

            gameOverScreen.SetActive(true);
            gameOverScreen.transform.position += new Vector3(0, 500, 0);
            gameOverScreen.transform.localScale -= new Vector3(0.8f, 0.8f, 0.8f);
            for (int i = 0; i < 15; i++)
            {
                yield return new WaitForSeconds(0.1f);
                if (i < 10)
                    gameOverScreen.transform.position -= new Vector3(0, 50, 0);
                if (i > 7)
                    gameOverScreen.transform.localScale += new Vector3(0.1f,0.1f,0.1f);
            }
        
    }

    public void FlashScreen()
    {
        StopCoroutine(FlashCoroutine());
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        Color c = flashScreen.color;
        c.a = 0f;
        for (int i = 0; i < 5; i++)
        {

            c.a = Mathf.Lerp(0f, 1f, i / 5f);
            flashScreen.color = c;
            yield return new WaitForSeconds(0.02f);

        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 10; i++)
        {

            c.a = Mathf.Lerp(1f, 0f, i / 10f);
            flashScreen.color = c;
            yield return new WaitForSeconds(0.02f);

        }
        c.a = 0f;
        flashScreen.color = c;
    }

    public void PauseGame()
    {

        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }
    

}
