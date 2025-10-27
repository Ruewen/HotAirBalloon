using UnityEngine;

public class HotAirBalloonController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Flight Settings")]
    [Tooltip("버너를 켰을 때 위로 가해지는 힘의 크기")]
    public float upwardForce = 20f;
    [Tooltip("하강 시 아래로 가하는 힘")]
    public float downwardForce = 5f;

    [Header("Ground Check (Raycast)")]
    [Tooltip("착륙 시 땅과 유지할 최소 거리")]
    public float landingBuffer = 1.0f; // 땅에서 1m 위에 떠서 멈춤
    [Tooltip("레이저가 감지할 땅(Ground) 레이어")]
    public LayerMask groundLayer;
    [Tooltip("레이저를 쏠 최대 거리")]
    public float groundCheckDistance = 1000f;

    [Header("Physics Settings")]
    public Transform centerOfMass;

    
    [Header("Sound Effects (Optional)")]
    public AudioSource burnerSound;
    public AudioSource ventSound;

    [Header("Visual Effects (Optional)")]
    public ParticleSystem burnerFireEffect;
    
    private bool isBurnerOn = false;
    private bool isVentOpen = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (centerOfMass != null)
        {
            rb.centerOfMass = centerOfMass.localPosition;
        }
    }

    void FixedUpdate()
    {
        float groundAltitude = -Mathf.Infinity; // 땅을 못 찾았을 때의 기본값
        bool isGroundDetected = false;

        RaycastHit hit;

        if (Physics.Raycast(rb.position, Vector3.down, out hit, groundCheckDistance, groundLayer))
        {
            groundAltitude = hit.point.y; // 부딪힌 지점의 Y좌표
            isGroundDetected = true;
        }

        float dynamicMinAltitude = groundAltitude + landingBuffer;

        Vector3 currentVelocity = Vector3.zero;

        // 1. 버너가 켜져 있으면 부력을 가한다.
        if (isBurnerOn)
        {
            currentVelocity.y = upwardForce;
        }
        else if (isVentOpen)
        {
            if (isGroundDetected && rb.position.y > dynamicMinAltitude)
            {
                currentVelocity.y = -downwardForce;
            }
        }
        Vector3 newPosition = rb.position + currentVelocity * Time.fixedDeltaTime;
        
        if (isGroundDetected && newPosition.y < dynamicMinAltitude)
        {
            newPosition.y = dynamicMinAltitude;
        }
    
        rb.MovePosition(newPosition);
    }

    public void StartAscending()
    {
        isBurnerOn = true;
        if (burnerSound != null && !burnerSound.isPlaying)
        {
            burnerSound.Play(); // 버너 사운드 재생
        }
        if (burnerFireEffect != null && !burnerFireEffect.isPlaying)
        {
            burnerFireEffect.Play();
        }
    }

    public void StopAscending()
    {
        isBurnerOn = false;
        if (burnerSound != null)
        {
            burnerSound.Stop(); // 버너 사운드 중지
        }

        if (burnerFireEffect != null)
        {
            burnerFireEffect.Stop();
        }
    }

    public void StartDescending()
    {
        isVentOpen = true;
        if (ventSound != null && !ventSound.isPlaying)
        {
            ventSound.Play(); // 벤트 사운드 재생
        }
    }

    public void StopDescending()
    {
        isVentOpen = false;
        if (ventSound != null)
        {
            ventSound.Stop(); // 벤트 사운드 중지
        }
    }
}