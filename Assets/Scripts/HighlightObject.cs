using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    MeshRenderer _renderer;

    Color originalColor;
    bool isHighlighted = false;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        originalColor = _renderer.material.color;
    }

    void Highlight()
    {
        isHighlighted = true;
        _renderer.material.color = highlightColor;
    }

    void Dehighlight()
    {
        isHighlighted = false;
        _renderer.material.color = originalColor;
        // Debug.Log("dehighlighted");
    }

    public void ToggleHighlight()
    {
        if (!isHighlighted)
        {
            Highlight();
        }
        else
        {
            Dehighlight();
        }
    }

    public Color GetOriginalColor()
    {
        return originalColor;
    }
}
