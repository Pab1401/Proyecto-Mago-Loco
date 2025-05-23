using UnityEngine;
using TMPro;

public class Interact_Manager : MonoBehaviour
{
    public TMP_Text ui_text;
    public playerController player;

    void Update()
    {
        if (player.canExit)
            ui_text.text = "Presiona 'E' o 'X' para salir";
        else if (player.canCollect)
            ui_text.text = "Presiona 'E' o 'X' para interactuar";
        else
            ui_text.text = "";
    }

}
