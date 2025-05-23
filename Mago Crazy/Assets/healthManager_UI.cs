using TMPro;
using UnityEngine;

public class healthManager_UI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public playerController player;
    public TMP_Text health_txt;
    void Update()
    {
        health_txt.text = "Vida: " + new string('█', player.health);
    }
    

}
