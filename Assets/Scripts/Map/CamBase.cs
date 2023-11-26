using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBase : MonoBehaviour
{
    public static CamBase main;

    [Header("GameObject")]
    [SerializeField]
    private PlayerMotor target;

    
    [Header("Stats")]
    [SerializeField]
    private float smoothing = 1f;
    [SerializeField]
    private float camCursorPreviewDist = 2f;
    [Header("Camera Shake")]
    [SerializeField]
    private float shakeTime;
    [SerializeField]
    private float shakeAmount;
    [SerializeField]
    private float shakeIncrement;

    [Header("Rotation")]
    [SerializeField]
    private float rotateSensitivity;
    [SerializeField]
    private float minRotationAngle;
    [SerializeField]
    private float maxRotationAngle;

    [SerializeField]
    private int rotateButton = 0;

    public Camera camera;


    // Rotation
    private Vector3 rotateStartPosition;
    private Vector3 rotateCurrentPosition;

    private Quaternion desiredRotation;

    public bool rotating { get; private set; }

    private float shake = 0f;


    private void Start()
    {
        main = this;
        camera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        Shake();
        FollowMovement();
        Rotate();
    }

    private void Shake()
    {
        if(shake > 0)
        {
            transform.localPosition = Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime;
        }
        else
            shake = 0;
    }

    private void FollowMovement()
    {
        transform.position = Vector3.Lerp(transform.position, 
            //Vector3.Lerp(target.transform.position, (target.transform.position + InputManager.MouseRaycastPlane()).normalized * camCursorPreviewDist, 0.5f )
            target.transform.position
            , Time.deltaTime * smoothing);
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(rotateButton))
        {
            if (!rotating)
            {
                rotateStartPosition = Input.mousePosition;
                rotating = true;
            }

            rotateCurrentPosition = Input.mousePosition;
            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            float xRotation = (desiredRotation.eulerAngles.x + (difference.y * rotateSensitivity) + 45) % 360f;

            if (xRotation > minRotationAngle && xRotation < maxRotationAngle)
                desiredRotation.eulerAngles += new Vector3(difference.y * rotateSensitivity, 0f, 0f);

            desiredRotation.eulerAngles += new Vector3(0f, -difference.x * rotateSensitivity, 0f);
        }
        else
            rotating = false;

        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * smoothing);
    }

    public static void CameraShake()
    {
        main.shake = main.shakeTime;
    }
}
