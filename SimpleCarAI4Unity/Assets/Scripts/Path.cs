using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    public Color linecolor;
    public List<Transform> nodes = new List<Transform>();


    private void OnDrawGizmosSelected()
    {
        {
            Gizmos.color = linecolor;
            Transform[] childTransform = transform.GetComponentsInChildren<Transform>();
            nodes = new List<Transform>();

            {
                for (int index = 0; index < childTransform.Length; index++)
                {
                    if (childTransform[index] != transform)
                        nodes.Add(childTransform[index]);
                }
            }

            {
                for (int index = 0; index < nodes.Count; index++)
                {
                    Vector3 currentNode = nodes[index].position;
                    Vector3 previousNode = Vector3.zero;

                    if (index > 0)
                    {
                        previousNode = nodes[index - 1].position;
                    }
                    else if (index == 0 && nodes.Count > 1)
                    {
                        previousNode = nodes[nodes.Count - 1].position;
                    }

                    Gizmos.DrawLine(previousNode, currentNode);
                    Gizmos.DrawWireSphere(currentNode, 0.1f);
                }
            }
        }
    }

    //private void OnDrawGizmos()

}
