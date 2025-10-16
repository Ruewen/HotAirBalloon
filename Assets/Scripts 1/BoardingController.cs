using System.Collections;
using UnityEngine;

public class BoardingController : MonoBehaviour
{
    [Header("필수 연결 요소")]
    public Transform playerRig;
    public Transform boardingTarget;
    public ScreenFader screenFader;
    public GameObject locomotionSystem;

    public void StartBoardingSequence()
    {
        StartCoroutine(BoardingSequence());
    }
    private IEnumerator BoardingSequence()
    {
        yield return StartCoroutine(screenFader.FadeOut());
        if(locomotionSystem != null)
        {
            locomotionSystem.SetActive(false);
        }

        playerRig.position = boardingTarget.position;
        playerRig.rotation = boardingTarget.rotation;

        yield return StartCoroutine(screenFader.FadeIn());
        Debug.Log("탑승 완료!");
    }
}
