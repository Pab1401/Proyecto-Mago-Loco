using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PropCombiner : MonoBehaviour
{
    [MenuItem("Tools/Combinar Props Seleccionados")]
    static void CombinarMeshes()
    {
        GameObject[] seleccionados = Selection.gameObjects;

        if (seleccionados.Length == 0)
        {
            Debug.LogWarning("Selecciona al menos un objeto con MeshFilter.");
            return;
        }

        List<CombineInstance> combineList = new List<CombineInstance>();
        Material materialCompartido = null;

        // Guardar referencias a los MeshFilters y Renderers antes de eliminar
        List<MeshFilter> meshFilters = new List<MeshFilter>();
        List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

        foreach (GameObject obj in seleccionados)
        {
            MeshFilter mf = obj.GetComponent<MeshFilter>();
            MeshRenderer mr = obj.GetComponent<MeshRenderer>();

            if (mf == null || mr == null)
                continue;

            meshFilters.Add(mf);
            meshRenderers.Add(mr);
        }

        if (meshFilters.Count == 0)
        {
            Debug.LogWarning("Ningún objeto seleccionado tiene MeshFilter y MeshRenderer.");
            return;
        }

        // Asumimos que todos comparten el mismo material para simplificar
        materialCompartido = meshRenderers[0].sharedMaterial;

        foreach (MeshFilter mf in meshFilters)
        {
            CombineInstance ci = new CombineInstance();
            ci.mesh = mf.sharedMesh;
            ci.transform = mf.transform.localToWorldMatrix;
            combineList.Add(ci);
        }

        Mesh combinadoMesh = new Mesh();
        combinadoMesh.CombineMeshes(combineList.ToArray());

        GameObject combinado = new GameObject("Props_Combinados");
        combinado.isStatic = true;

        MeshFilter mfFinal = combinado.AddComponent<MeshFilter>();
        mfFinal.sharedMesh = combinadoMesh;

        MeshRenderer mrFinal = combinado.AddComponent<MeshRenderer>();
        mrFinal.sharedMaterial = materialCompartido;

        // Ahora eliminamos los objetos originales (ya que tenemos las referencias guardadas)
        foreach (GameObject obj in seleccionados)
        {
            DestroyImmediate(obj);
        }

        Debug.Log("¡Props combinados y originales eliminados exitosamente!");
    }
}