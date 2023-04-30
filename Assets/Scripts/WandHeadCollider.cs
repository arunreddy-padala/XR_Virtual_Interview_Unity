using UnityEngine;

public class WandHeadCollider : MonoBehaviour
{

    // input the meshrenderer of the wand head
    public MeshRenderer _renderer;

    Color wandColor;

    // the traget render
    private Renderer targetRenderer;

    private void OnTriggerEnter(Collider other)
    {
        _renderer = GetComponent<MeshRenderer>();
        // get the sphere head color
        wandColor = _renderer.material.color;


        // need wand detection comp in other object to change color
        if (other.gameObject.GetComponent<WandDetectionComponent>() != null)
        {
            // change the target render material to the wand color
            targetRenderer = other.GetComponent<Renderer>();
            if (targetRenderer != null)
            {
                targetRenderer.material.color = wandColor;
            }
        }
    }

}
