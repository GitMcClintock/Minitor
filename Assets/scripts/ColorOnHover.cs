using UnityEngine;
using System.Linq;
/* Tint the object when hovered. */

public class ColorOnHover : MonoBehaviour
{

    public Color color;
    public Renderer meshRenderer;

    Color[] originalColours;
    Behaviour halo;

    void Start()
    {
        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        originalColours = meshRenderer.materials.Select(x => x.color).ToArray();

        var behaviours = GetComponents<Behaviour>();
        foreach(var b in behaviours)
        {
            //Debug.Log("Found behaviour on " + b.name + ", " + b.GetType());
            if(b.GetType().ToString() == "UnityEngine.Halo")
            {
                halo = b;
            }
        }
    }

    void OnMouseEnter()
    {
        foreach (Material mat in meshRenderer.materials)
        {
            mat.color *= color;
        }

        if (halo != null) { halo.enabled = true; }
    }

    void OnMouseExit()
    {
        for (int i = 0; i < originalColours.Length; i++)
        {
            meshRenderer.materials[i].color = originalColours[i];
        }
        if (halo != null) { halo.enabled = false; }
    }

}
