using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerLoader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeDisplay;
    private float timePlayed;

    void Start()
    {
        timePlayed = PlayerPrefs.GetFloat("TimePlayed", 0);
        int minute = Mathf.FloorToInt(timePlayed / 60);
        int second = Mathf.FloorToInt(timePlayed % 60);
        timeDisplay.text = minute + ":" + second;
    }
}
