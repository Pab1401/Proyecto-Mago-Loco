using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using UnityEngine;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public class boxController : MonoBehaviour
{
    private bool hasCollided;
    private Rigidbody rb;
    private int counter;
    private Vector3[] _vlist = new Vector3[4];
    public Vector3[] VList
    {
        set {_vlist = value;}
        get {return _vlist;}
    }
    private Vector3 _direction;
    public Vector3 Direction
    {
        set {_direction = value;}
        get {return _direction;}
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collided");
        //Debug.Log("Counter: " + counter);
        if (hasCollided)
            return;
        if (counter == VList.Length - 1)
            counter = 0;
        else
            counter++;
        hasCollided = true;
        //Debug.Log("Counter after: " + counter);
    }
    void OnTriggerExit(Collider other)
    {
        hasCollided = false;
    }

    void Start()
    {
        counter = 0;
        rb = GetComponent<Rigidbody>();
        Vector3 temp = new Vector3(1, 0, 0);
        VList[0] = temp;
        temp = new Vector3(0, 0, 1);
        VList[1] = temp;
        temp = new Vector3(-1, 0, 0);
        VList[2] = temp;
        temp = new Vector3(0, 0, -1);
        VList[3] = temp;
        //Debug.Log(VList[0] + " ; " + VList[1]);
        Vector3 Direction = VList[counter];
        
    }

    void Update()
    {
        Direction = VList[counter];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = Direction * 10;
    }
}
