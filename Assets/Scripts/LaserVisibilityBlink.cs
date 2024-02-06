using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserVisibilityBlink : MonoBehaviour
{
    public float visibleDuration = 2f;
    public float invisibleDuration = 2f;

    private MeshRenderer meshRenderer;
    private Collider colliderComponent;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        colliderComponent = GetComponent<Collider>();

        StartCoroutine(ToggleVisibility());
    }

    private IEnumerator ToggleVisibility()
    {
        while (true)
        {
            // Make laser visible and collidable
            SetLaserState(true);

            yield return new WaitForSeconds(visibleDuration);

            // Make laser invisible and not collidable
            SetLaserState(false);

            yield return new WaitForSeconds(invisibleDuration);
        }
    }

    private void SetLaserState(bool state)
    {
        if (meshRenderer != null)
            meshRenderer.enabled = state;

        if (colliderComponent != null)
            colliderComponent.enabled = state;
    }
}
