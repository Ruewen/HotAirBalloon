using UnityEngine;

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

    
    [Header("Sound Effects (Optional)")]
    public AudioSource burnerSound;
    public AudioSource ventSound;
    
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

    public void StartAscending()
    {
        Debug.Log("상승 시작!");
        isBurnerOn = true;
        if (burnerSound != null && !burnerSound.isPlaying)
        {
            burnerSound.Play(); // 버너 사운드 재생
        }
    }

    public void StopAscending()
    {
        isBurnerOn = false;
        if (burnerSound != null)
        {
            burnerSound.Stop(); // 버너 사운드 중지
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