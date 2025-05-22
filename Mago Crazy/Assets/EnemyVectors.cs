using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectScript", menuName = "Scriptable Objects/ScriptableObjectScript")]
public class ScriptableObjectScript : ScriptableObject
{
    public GameObject[] objetos;
    public List<Vector3> GetVectors()
    {
        List<Vector3> vectores = new List<Vector3>();
        for (int i = 0; i < objetos.Length - 1; i++)
        {
            if (objetos[i] != null && objetos[i + 1] != null)
            {
                Vector3 direction = objetos[i + 1].transform.position - objetos[i].transform.position;
                vectores.Add(direction.normalized);
            }
        }
        return vectores;
    }
}