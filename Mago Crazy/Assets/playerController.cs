using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    Rigidbody rb;
    Camera mainCam;
    GameObject player;
    Vector3 cameraForward, cameraRight;
    float horizontalInput;
    float verticalInput;
    int health;
    float playerSpeed = 5f;
    bool hasCollided = false;
    bool alive = true;
    bool shouldCollect = false;
    bool canCollect = false;
    bool canExit = false;
    bool shouldExit = false;
    float sensitivity;
    int collectedTotal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 3;
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
        if (!alive)
        {
            Debug.Log("Dead");
        }
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        cameraForward = mainCam.transform.forward;
        cameraRight = mainCam.transform.right;

        cameraForward = Vector3.ProjectOnPlane(cameraForward, Vector3.up).normalized;
        cameraRight = Vector3.ProjectOnPlane(cameraRight, Vector3.up).normalized;

        Vector3 moveDirection = (verticalInput * cameraForward) + (horizontalInput * cameraRight);
        player.transform.Translate(moveDirection.normalized * playerSpeed * Time.deltaTime, Space.World);

        float rotateHorizontal = Input.GetAxis("Mouse X");
        transform.Rotate(0, rotateHorizontal * sensitivity, 0, Space.Self);

        if (canCollect && Input.GetKeyDown(KeyCode.E))
        {
            shouldCollect = true;
        }
        if (canExit && Input.GetKeyDown(KeyCode.E))
        {
            shouldExit = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (shouldExit)
        {

        }
        if (shouldCollect)
            {
                shouldCollect = false;
                canCollect = false;
                Destroy(other.gameObject);
                lightController.Instance.AdjustLight();
                collectedTotal++;
                Debug.Log(collectedTotal);
            }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (hasCollided)
            return;
        if (collision.gameObject.tag == "enemy")
        {
            health--;
            if (health == 0)
                alive = false;
            Vector3 knockback = new Vector3(-1 * (this.transform.position.x - collision.gameObject.transform.position.x), 0 , -1 * (this.transform.position.y - collision.gameObject.transform.position.y)).normalized;
            Debug.Log("knockback: "+knockback);
            rb.AddForce(knockback * 100, ForceMode.Impulse);
            hasCollided = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("wont collide");
            new WaitForSeconds(100);
            Debug.Log("can collide again");
            hasCollided = false;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (collectedTotal == 3 && other.tag == "finish")
        {
            canExit = true;
        }   
        if (other.tag == "collect")
            canCollect = true;
    }
    void OnTriggerExit(Collider other)
    {
        canExit = false;
        canCollect = false;
    }
}
