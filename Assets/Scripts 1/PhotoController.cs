using System.Collections;
using System.IO;
using UnityEngine;

public class PhotoController : MonoBehaviour
{
    [Header("카메라 설정")]
    [Tooltip("사진을 찍는 데 사용할 카메라")]
    public Camera photoCamera;

    [Header("사진 저장 설정")]
    [Tooltip("사진이 그려질 렌더 텍스쳐")]
    public RenderTexture renderTexture;
    [Tooltip("사진 파일 이름의 접두사")]
    public string photoNamePrefix = "MyVRPhoto-";

    [Header("입력")]
    [Tooltip("사진 촬영을 위한 키 (테스트용)")]
    public KeyCode captureKey = KeyCode.Mouse0;

    [Header("피드백 효과")]
    [Tooltip("사진 찍을 때 재생할 소리")]
    public AudioSource shutterSound;

    void Update()
    {
        if(Input.GetKeyDown(captureKey))
        {
            StartCoroutine(CapturePhoto());
        }
    }

    private IEnumerator CapturePhoto()
    {
        if (shutterSound != null)
        {
            shutterSound.Play();
        }
        photoCamera.Render();

        RenderTexture.active = renderTexture;
        Texture2D photo = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        photo.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        photo.Apply();
        RenderTexture.active = null;

        byte[] bytes = photo.EncodeToPNG();
        Destroy(photo);

        string fileName = $"{photoNamePrefix}{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, fileName);
        System.IO.File.WriteAllBytes(filePath, bytes);

        Debug.Log($"사진이 파일로 저장되었습니다: {filePath}");

        yield return null;
    }
}
