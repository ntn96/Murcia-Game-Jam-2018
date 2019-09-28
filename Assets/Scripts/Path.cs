using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    public List<Vector3> actualPath = new List<Vector3>();
    [SerializeField] Color gizmosColor = Color.blue;
    [SerializeField] float gizmosRadius = 0.5f;
    [SerializeField] bool debug = false; 

    void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = gizmosColor;
            for (int i = 0; i < actualPath.Count; i++)
            {
                Gizmos.DrawSphere(actualPath[i], gizmosRadius);
                if (i != 0 && i < actualPath.Count)
                {
                    Gizmos.DrawLine(actualPath[i - 1], actualPath[i]);
                }
            }
        }
    }
}
