using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventManager : MonoBehaviour
{
    public UnityEvent onStartScene;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        onStartScene.Invoke();
    }
}
