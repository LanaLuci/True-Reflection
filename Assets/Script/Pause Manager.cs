using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    public static bool gamePaused {  get; private set; }
    public UnityEvent onPause;

    void Awake()
    {
        gamePaused = false;
    }

    void Update()
    {
        if (Monster.playerCaught)
            return;
        if (Input.GetKeyDown(KeyCode.P))
        {
            onPause.Invoke();
            Cursor.visible = gamePaused;
            if (Cursor.visible)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

        }
    }
    public void Pause()
    {
        gamePaused = !gamePaused;
    }
}
