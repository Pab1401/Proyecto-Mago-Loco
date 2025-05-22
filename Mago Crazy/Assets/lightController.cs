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
        foreach (Light item in sceneLights)
        {
            item.intensity = 150f;
            item.range = 20f;
        }
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void AdjustLight()
    {
        totalCollected++;
        float newIntensity = Mathf.Max(1f, 150f - 45f * totalCollected);

         foreach (Light light in sceneLights)
        {
            if (light != null)
                light.intensity = newIntensity;
        }

    }
}
