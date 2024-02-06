using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NavMeshAgentVision_v2 : ManagedClass
{
    public LayerMask visibleLayers;
    public LayerMask obstacleLayers;
    public float viewRadius;
    public float viewDegrees;


    //added event system to notify other scripts when an object is visible or not
    public static event Action<string, bool> ObjectVisibilityChanged;
    public static void NotifyVisibilityChange(string objectName, bool isVisible)
    { 
        ObjectVisibilityChanged?.Invoke(objectName, isVisible);
    }


    [SerializeField] private bool draw;

    public void Update()
    {
        //get parent position
        Vector3 parentPosition = transform.root.position;


        Collider[] visibleColliders = Physics.OverlapSphere(parentPosition, viewRadius, visibleLayers);
        if (visibleColliders.Length > 0)
        {
            for (int i = 0; i < visibleColliders.Length; i++)
            {
                Vector3 currentColliderPos = visibleColliders[i].transform.position;

                //added this to get name of colliding object
                string targetName = visibleColliders[i].gameObject.name;

                if (Physics.Linecast(parentPosition, currentColliderPos, obstacleLayers))
                {
                    //there's an obstacle between the agent and the collider
                    if (draw)
                    {
                        Debug.DrawLine(parentPosition, currentColliderPos, Color.red);
                        //added this to notify other scripts that the object is not visible
                        NotifyVisibilityChange(targetName, false);
                    }
                    continue;
                }
                else
                {
                    //there are no obstacles between the agent and the collider
                    //check if visible object is in front
                    if (Mathf.Abs(Vector3.Angle(transform.forward, currentColliderPos - parentPosition)) < viewDegrees)
                    {
                        //in agent's field of view
                        //do something
                        if (draw)
                        {
                            Debug.DrawLine(parentPosition, currentColliderPos, Color.green);
                            //added this to notify other scripts that the object is visible
                            NotifyVisibilityChange(targetName, true);
                        }
                    }
                    else
                    {
                        if (draw)
                        {
                            Debug.DrawLine(parentPosition, currentColliderPos, Color.red);
                            //added this to notify other scripts that the object is not visible
                            NotifyVisibilityChange(targetName, false);
                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {       
        Vector3 parentPosition = transform.root.position;

        Gizmos.DrawWireSphere(parentPosition, viewRadius);
    }
}
