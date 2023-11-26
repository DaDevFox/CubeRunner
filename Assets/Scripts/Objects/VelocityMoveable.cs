using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class GamePhysics
{
    public static List<Collider> physicsBodies = new List<Collider>();

    public static void TrackAll()
    {
        var bodies = GameObject.FindObjectsOfType(typeof(Collider));
        physicsBodies.Clear();
        foreach (var body in bodies)
            physicsBodies.Add(body as Collider);
    }
}






public class PlayerMotor : MonoBehaviour
{
    [Header("Motor Settings")]
    [SerializeField]
    private Space translationSpace = Space.Self;
    [SerializeField]
    private float drift = 1f;
    [SerializeField]
    private float rotationDrift = 10f;

    /// <summary>
    /// Desired (unsmoothed) amount of velocity this frame
    /// </summary>
    public Vector3 desiredPosition { get; set; }

    /// <summary>
    /// (smoothed) amount of movement that happened last frame
    /// </summary>
    public Vector3 delta { get; private set; }
    private Vector3 lastFrame;

    /// <summary>
    /// Desired (unsmoothed) amount of velocity this frame
    /// </summary>
    public Quaternion desiredRotation { get; set; }

    private Rigidbody rb;

    private void Start()
    {
        desiredPosition = transform.position;

        this.rb = this.gameObject.AddComponent<Rigidbody>();
        this.rb.useGravity = false;
        this.rb.drag = 1f;
        this.rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ;
    }


    private void Update()
    {
        DoSmoothMovement();
        DoSmoothRotation();

        OnUpdate();
    }

    private void DoSmoothMovement()
    {
        float bound = 20f;
        Vector3 bounds = Map.Size;

        desiredPosition = new Vector3(
            Mathf.Clamp(desiredPosition.x, -bounds.x, bounds.x),
            Mathf.Clamp(desiredPosition.y, -bounds.y, bounds.y),
            Mathf.Clamp(desiredPosition.z, -bounds.z, bounds.z));

        //Smooth Movement
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * drift);
        

        //Smooth Rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationDrift);
        transform.rotation.SetAxisAngle(new Vector3(0f, 0f, 1f), 0f);




        // Delta
        delta = transform.position - lastFrame;
        lastFrame = transform.position;
    }

    private void DoSmoothRotation()
    {

    }



    protected virtual void OnUpdate()
    {

    }
}

