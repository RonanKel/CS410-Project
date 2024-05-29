using UnityEngine;

public class TransparencyController : MonoBehaviour
{
    public Material targetMaterial;
    public float transparency = 0.5f; // Set this value between 0 (fully transparent) and 1 (opaque)

    void Update()
    {
        if (targetMaterial != null)
        {
            Color color = targetMaterial.color;
            color.a = transparency; // Adjust alpha based on the transparency variable
            targetMaterial.color = color;
        }
    }
}

