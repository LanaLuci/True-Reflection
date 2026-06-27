using UnityEngine;
using UnityEngine.Events;

public class EvtMovable : MonoBehaviour
{
    public void MoveX(float x)
    {
        gameObject.transform.position += new Vector3(x,0,0);
    }
    public void MoveY(float y)
    {
        gameObject.transform.position += new Vector3(0,y,0);
    }
    public void MoveZ(float z)
    {
        gameObject.transform.position += new Vector3(0,0,z);
    }
}
