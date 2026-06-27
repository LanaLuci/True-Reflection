using UnityEngine;
using System.Collections;

public class MirrorManager : MonoBehaviour
{
    [SerializeField] private Camera mirrorCam;
    [SerializeField] private float maxDist;
    //[SerializeField] private RenderTexture texture;
    public static MirrorManager instance;
    private Mirror[] mirrors;

    IEnumerator Start()
    {
        instance = this;   
        //mirrorCam.targetTexture = texture;
        for (int i = 0; i < 3; i++)
        {
            yield return null;
        }
        mirrors = FindObjectsByType<Mirror>(FindObjectsSortMode.None);
        //if (mirrors == null)
        //    Debug.LogWarning("0");


    }

    //public RenderTexture GetTexture() { return texture; }

    private void LateUpdate()
    {
        if (mirrors == null)
            return;

        //Mirror bestMirror = null;

        //float bestDistance = Mathf.Infinity;

        Vector3 playerPos = Camera.main.transform.position;

        for (int i = 0; i < mirrors.Length; i++)
        {

            float dist = Vector3.Distance(playerPos, mirrors[i].transform.position);
            //Debug.Log(dist);


            if (dist > maxDist)
                continue;


            Vector3 dir =
                            (playerPos - mirrors[i].transform.position).normalized;

            float dot =
                Vector3.Dot(mirrors[i].transform.forward, dir);

            if (dot < 0)
                continue;

            RenderMirror(mirrors[i]);

            if (i == mirrors.Length -1)
                RenderMirror(mirrors[i]);

            //if (dist < bestDistance)
            //{
            //    bestDistance = dist;
            //    bestMirror = mirrors[i];
            //}
        }

        //if (bestMirror != null)
        //{

        //    bestMirror.UpdateMirror();
        //}
    }

    public void RenderMirror(Mirror cam)
    {

        mirrorCam.targetTexture = cam.Texture;
        mirrorCam.transform.position = cam.transform.position;
        mirrorCam.transform.rotation = cam.transform.rotation * Quaternion.Euler(-90, 0, 0);
        mirrorCam.Render();

    }

}
