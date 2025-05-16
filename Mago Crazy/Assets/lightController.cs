using System.Linq;
using System.Threading;
using UnityEngine;

public class lightController : MonoBehaviour
{
    [Header("Light to control")]
    public Light[] sceneLights;
    public static lightController Instance;
    int totalCollected;
    void Start()
    {
        sceneLights = new Light[GameObject.FindGameObjectsWithTag("light").Length];
        sceneLights = GameObject.FindGameObjectsWithTag("light")
                                .Select(go => go.GetComponent<Light>())
                                .Where(light => light != null)
                                .ToArray();
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void AdjustLight()
    {
        totalCollected++;
        float newIntensity = Mathf.Max(1f, 32f - 8f * totalCollected);

         foreach (Light light in sceneLights)
        {
            if (light != null)
                light.intensity = newIntensity;
        }

    }
}
