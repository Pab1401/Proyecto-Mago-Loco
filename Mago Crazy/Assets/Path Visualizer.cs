using UnityEngine;

public class PathVisualizer : MonoBehaviour
{
    public ScriptableObjectScript pathData;

    private void OnDrawGizmos()
    {
        if (pathData == null || pathData.objetos == null || pathData.objetos.Length < 2)
            return;

        Gizmos.color = Color.cyan;

        for (int i = 0; i < pathData.objetos.Length - 1; i++)
        {
            GameObject a = pathData.objetos[i];
            GameObject b = pathData.objetos[i + 1];

            if (a != null && b != null)
                Gizmos.DrawLine(a.transform.position, b.transform.position);
        }
    }
}
