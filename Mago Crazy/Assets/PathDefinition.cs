using System.Collections.Generic;
using UnityEngine;

public class PathDefinition : MonoBehaviour
{
    public Transform[] pathPoints;
    public bool loopToStart = true; // toggle this in the Inspector

    public Vector3[] GetVectors()
{
    List<Vector3> vectors = new List<Vector3>();

    for (int i = 0; i < pathPoints.Length - 1; i++)
    {
        if (pathPoints[i] != null && pathPoints[i + 1] != null)
        {
            Vector3 dir = pathPoints[i + 1].position - pathPoints[i].position;
            vectors.Add(dir.normalized);
        }
    }

    // Add final direction from last point to first
    if (pathPoints.Length >= 2 && pathPoints[0] != null && pathPoints[^1] != null)
    {
        Vector3 finalDir = pathPoints[0].position - pathPoints[^1].position;
        vectors.Add(finalDir.normalized);
    }

    return vectors.ToArray();
}

}
