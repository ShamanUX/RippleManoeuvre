using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ChangeColor : MonoBehaviour
{
    [Header("Color Settings")]
    public Color originalColor = Color.white;
    public Color changedColor = Color.red;

    [Header("References")]
    public Renderer targetRenderer;

    private bool isRed = false;
    private Material material;

    void Start()
    {
        // Auto-get renderer if not assigned
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();

        if (targetRenderer != null)
        {
            material = targetRenderer.material;
            originalColor = material.GetColor("_BaseColor");
        }
    }

    // Public API Methods

    public void ToggleColor()
    {
        if (isRed)
            RevertToOriginalColor();
        else
            ChangeToRed();
    }

    public void ChangeToRed()
    {
        if (material != null)
        {
            material.SetColor("_BaseColor", changedColor);
            isRed = true;
        }
    }
    public void RevertToOriginalColor()
    {
        if (material != null)
        {
            material.SetColor("_BaseColor", originalColor);
            isRed = false;
        }
    }

    public bool IsRed()
    {
        return isRed;
    }
}