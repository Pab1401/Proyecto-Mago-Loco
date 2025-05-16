using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody rb;
    Camera mainCam;
    GameObject player;
    Vector3 cameraForward, cameraRight;
    float horizontalInput;
    float verticalInput;
    float playerSpeed = 5f;
    bool hasCollided = false;
    bool isCollecting = false;
    bool shouldCollect = false;
    bool canCollect = false;
    float sensitivity;
    int collectedTotal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sensitivity = 2.5f;
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

        Vector3 moveDirection = (verticalInput * cameraForward) + (horizontalInput * cameraRight);
        player.transform.Translate(moveDirection.normalized * playerSpeed * Time.deltaTime, Space.World);

        float rotateHorizontal = Input.GetAxis ("Mouse X");
        transform.Rotate(0, rotateHorizontal * sensitivity ,0, Space.Self);

        if (canCollect && Input.GetKeyDown(KeyCode.E))
        {
            shouldCollect = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (shouldCollect && !isCollecting)
        {
            shouldCollect = false;
            isCollecting = true;
            Destroy(other.gameObject);
            lightController.Instance.AdjustLight();
            collectedTotal++;
            Debug.Log(collectedTotal);
        }
        if (other.tag == "collect")
            canCollect = true; Debug.Log("CanCollect");
    }
    void OnTriggerEnter(Collider other)
    {
        if(hasCollided)
            return;
        if (other.tag == "enemy")
        {
            Vector3 knockback = new Vector3(-1 * (this.transform.position.x - other.transform.position.x), 0 , -1 * (this.transform.position.y - other.transform.position.y)).normalized;
            rb.AddForce(knockback * 100, ForceMode.Impulse);
            hasCollided = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        hasCollided = false;
        isCollecting = false;
        canCollect = false;
    }
}
