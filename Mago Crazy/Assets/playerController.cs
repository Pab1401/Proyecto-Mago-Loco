using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class playerController : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction m_moveAround;
    private InputAction m_lookAround;
    private InputAction m_interact;
    private InputAction m_pause;
    private Vector2 m_moveV;
    public bool isPaused;
    Rigidbody rb;
    Camera mainCam;
    GameObject player;
    Vector3 cameraForward, cameraRight;
    float horizontalInput;
    float verticalInput;
    public int health;
    float playerSpeed = 5.5f;
    bool hasCollided = false;
    bool alive = true;
    bool shouldCollect = false;
    public bool canCollect = false;
    public bool canExit = false;
    bool shouldExit = false;
    float sensitivity;
    public int collectedTotal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }
    public void Paused()
    {
        isPaused = !isPaused; // Toggle the state
        UnityEngine.Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        UnityEngine.Cursor.visible = isPaused;
        Time.timeScale = isPaused ? 0f : 1f; // Pause (0) or unpause (1)
    }
    void Awake()
    {
        isPaused = false;
        m_moveAround = InputSystem.actions.FindAction("Move");
        m_lookAround = InputSystem.actions.FindAction("Look");
        m_interact = InputSystem.actions.FindAction("Interact");
        m_pause = InputSystem.actions.FindAction("Pause");
    }

    void Start()
    {
        health = 3;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        sensitivity = 1f;
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
            SceneManager.LoadScene("Lose");
        }
        m_moveV = m_moveAround.ReadValue<Vector2>();

        cameraForward = mainCam.transform.forward;
        cameraRight = mainCam.transform.right;

        cameraForward = Vector3.ProjectOnPlane(cameraForward, Vector3.up).normalized;
        cameraRight = Vector3.ProjectOnPlane(cameraRight, Vector3.up).normalized;

        Vector3 moveDirection = (m_moveV.y * cameraForward) + (m_moveV.x * cameraRight);
        player.transform.Translate(moveDirection.normalized * playerSpeed * Time.deltaTime, Space.World);

        float rotateHorizontal = m_lookAround.ReadValue<Vector2>().x;
        //float rotateHorizontal = Input.GetAxis("Mouse X");

        transform.Rotate(0, rotateHorizontal * sensitivity  * (isPaused ? 0 : 1), 0, Space.Self);

        if (m_pause.WasPressedThisFrame())
        {
            Paused();
        }

        if (canCollect && m_interact.WasPressedThisFrame())
        {
            shouldCollect = true;
        }
        if (canExit && m_interact.WasPressedThisFrame())
        {
            shouldExit = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (shouldExit)
        {
            SceneManager.LoadScene("Win");
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
            new WaitForSeconds(3000);
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
