using UnityEngine;

public class CollisionComponentDector : MonoBehaviour
{
    public Collider targetCollider;
    MeshRenderer _render;
    Color targetColor;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == targetCollider)
        {
            Debug.Log("Collision detected with component: " + targetCollider.gameObject.name);

            _render = targetCollider.gameObject.GetComponent<MeshRenderer>();
            targetColor = _render.material.color;
            Debug.Log("target color: " + targetColor);
        }
    }
}