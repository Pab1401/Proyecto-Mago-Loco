using TMPro;
using UnityEngine;

public class collectible_counter : MonoBehaviour
{
    public playerController player;
    public TMP_Text collection;
    void Update()
    {
        collection.text = "Items: " + new string('█', player.collectedTotal) + new string('░', 3 - player.collectedTotal);
    }
}
