using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scroll_Speed = 0.3f;

    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Scroll();
    }

    private void Scroll()
    {
        Vector2 offset = meshRenderer.sharedMaterial.GetTextureOffset("_MainTex");
        offset.y += Time.deltaTime * scroll_Speed;

        meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
