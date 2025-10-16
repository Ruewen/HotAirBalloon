using UnityEngine;
using UnityEngine.Events; // UnityEvent를 사용하기 위해 꼭 필요합니다.

public class VRWorldButton : MonoBehaviour
{
    // 인스펙터 창에 노출되어, 원하는 기능을 연결할 수 있는 이벤트 슬롯입니다.
    public UnityEvent onButtonClick;

    // XR Grab Interactable이 이 함수를 호출해줍니다.
    public void Click()
    {
        // 이 함수가 호출되면, 인스펙터 창에 연결된 모든 기능들을 실행시킵니다.
        onButtonClick.Invoke();
    }
}
