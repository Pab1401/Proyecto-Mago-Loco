using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody rb;
    Camera mainCam;
    GameObject player;
    Vector3 cameraForward, cameraRight;
    float horizontalInput;
    float verticalInput;
    float playerSpeed = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = Camera.main;
        player = gameObject;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        cameraForward = mainCam.transform.forward;
        cameraRight = mainCam.transform.right;

        cameraForward = Vector3.ProjectOnPlane(cameraForward, Vector3.up).normalized;
        cameraRight = Vector3.ProjectOnPlane(cameraRight, Vector3.up).normalized;
    }

    void FixedUpdate()
    {
        player.transform.Translate(((verticalInput * cameraForward) + (horizontalInput * cameraRight)).normalized);
    }
}
