using UnityEngine;

public class cameraController : MonoBehaviour
{
    GameObject player;
    float sensitivity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sensitivity = 1.2f;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float rotateHorizontal = Input.GetAxis ("Mouse X");
		//float rotateVertical = Input.GetAxis ("Mouse Y");
        transform.RotateAround (player.transform.position, transform.up, rotateHorizontal * sensitivity); //use transform.Rotate(-transform.up * rotateHorizontal * sensitivity) instead if you dont want the camera to rotate around the player
		//transform.RotateAround (player.transform.position, transform.right, rotateVertical * sensitivity); // again, use transform.Rotate(transform.right * rotateVertical * sensitivity) if you don't want the camera to rotate around the player

    }
}
