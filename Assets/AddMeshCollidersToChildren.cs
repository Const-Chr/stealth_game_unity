using UnityEngine;

public class AddMeshCollidersToChildren : MonoBehaviour
{
    void Start()
    {
        // Get all child objects of the current GameObject
        Transform[] children = GetComponentsInChildren<Transform>();

        // Loop through each child
        foreach (Transform child in children)
        {
            // Add a Mesh Collider component to the child
            MeshCollider meshCollider = child.gameObject.AddComponent<MeshCollider>();

            // You can configure Mesh Collider properties here if needed
            // For example:
            // meshCollider.convex = true;
        }
    }
}
