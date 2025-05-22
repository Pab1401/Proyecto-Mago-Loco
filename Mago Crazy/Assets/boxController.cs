using UnityEngine;

public class boxController : MonoBehaviour
{
    public PathDefinition pathDefinition; // Reference to scene MonoBehaviour
    private GameObject enemy;
    private bool hasCollided;
    private Rigidbody rb;
    private int counter;

    private Vector3[] _vlist;
    public Vector3[] VList
    {
        set { _vlist = value; }
        get { return _vlist; }
    }

    private Vector3 _direction;
    public Vector3 Direction
    {
        set { _direction = value; }
        get { return _direction; }
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasCollided)
            return;

        if (other.CompareTag("checkpoint"))
        {
            counter = (counter + 1) % VList.Length;
            hasCollided = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        hasCollided = false;
    }

    void Start()
    {
        enemy = gameObject;
        counter = 0;
        rb = GetComponent<Rigidbody>();

        if (pathDefinition != null)
        {
            VList = pathDefinition.GetVectors();
        }
        else
        {
            Debug.LogWarning("PathDefinition not assigned!");
            VList = new Vector3[0];
        }

        Direction = VList.Length > 0 ? VList[counter] : Vector3.zero;
    }

    void Update()
    {
        if (VList.Length > 0)
            Direction = VList[counter];
    }

    void FixedUpdate()
    {
        rb.linearVelocity = Direction * 10f; // Note: use rb.velocity, not linearVelocity
    }
}
