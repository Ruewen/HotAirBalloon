using UnityEngine;
using UnityEngine.UI;

public class HotAirBalloonController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Flight Settings")]
    [Tooltip("버너를 켰을 때 위로 가해지는 힘의 크기")]
    public float upwardForce = 20f;
    [Tooltip("하강 시 아래로 가하는 힘")]
    public float downwardForce = 5f;

    [Header("Physics Settings")]
    public Transform centerOfMass;

    [Header("VR Input")]
    [Tooltip("상승을 위한 키")]
    public KeyCode burnerKey = KeyCode.Space;
    [Tooltip("하강을 위한 키")]
    public KeyCode ventKey = KeyCode.LeftShift;
    
    private bool isBurnerOn = false;
    private bool isVentOpen = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (centerOfMass == null)
        {
            rb.centerOfMass = centerOfMass.localPosition;
        }
    }

    void Update()
    {
        isBurnerOn = Input.GetKey(burnerKey);
        isVentOpen = Input.GetKey(ventKey);
    }

    void FixedUpdate()
    {
        // 1. 버너가 켜져 있으면 부력을 가한다.
        if (isBurnerOn)
        {
            // ForceMode.Force는 질량에 영향을 받으므로 사실적인 느낌을 줍니다.
            rb.AddForce(Vector3.up * upwardForce, ForceMode.Force);
        }
        if (isVentOpen)
        {
            rb.AddForce(Vector3.down * downwardForce, ForceMode.Force);
        }
    }
}