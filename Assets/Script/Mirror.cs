using UnityEngine;

public class Mirror : MonoBehaviour
{

    private Renderer rend;
    private Material mat;
    private RenderTexture texture;

    public RenderTexture Texture => texture;
    void Start()
    {
        //renderer = GetComponent<MeshRenderer>();
        //mat = new Material(renderer.material);
        //mat.mainTexture = MirrorManager.instance.GetTexture();
        //renderer.material = mat;
        rend = GetComponent<MeshRenderer>();

        mat = new Material(rend.material);

        texture = new RenderTexture(256, 256, 16);

        mat.mainTexture = texture;

        rend.material = mat;
    }

    //public void UpdateMirror()
    //{

    //    MirrorManager.instance.RenderMirror(this.transform);
    //}
}
