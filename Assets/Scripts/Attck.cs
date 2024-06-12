using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Attck : MonoBehaviour
{
    // Start is called before the first frame update
    public ARObjectClickHandler arObjectClickHandler;
    public ARPlaceOnPlane arPlaceOnPlane;
    public CameraFilter arCameraFilter;
    public float detectionWidth = 0.5f; // 네모 반경의 폭 (화면 너비의 비율)
    public float detectionHeight = 0.5f; // 네모 반경의 높이 (화면 높이의 비율)

    public AudioClip cameraShutterSound;  // Inspector에서 할당할 사운드 클립
    private AudioSource audioSource;
    public GameObject flashGameObj;
    public Image flashImage; // 화면 밝아짐 효과를 위한 이미지
    public float flashDuration = 0.5f; // 화면 밝아짐 효과 지속 시간
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = cameraShutterSound;
        if (flashImage != null)
        {
            flashImage.color = new Color(1, 1, 1, 0); // 초기 색상을 투명하게 설정
        }
    }
    public void TriggerCameraEffect()
    {
        if (cameraShutterSound != null)
        {
            audioSource.PlayOneShot(cameraShutterSound);
        }

        if (flashImage != null)
        {
            StartCoroutine(FlashEffect());
        }
    }
    private IEnumerator FlashEffect()
    {
        // 화면 밝아짐 효과
        flashGameObj.SetActive(true);
        flashImage.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(flashDuration / 2);

        // 화면 원래대로 돌아옴
        flashImage.color = new Color(1, 1, 1, 0);
        flashGameObj.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private bool DetectGhost(int panel)
    {

        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 worldCenter = Camera.current.ScreenToWorldPoint(screenCenter);

        // 감지 영역 설정
        float screenWidth = Screen.width * detectionWidth;
        float screenHeight = Screen.height * detectionHeight;


        foreach (var spawnGhost in arPlaceOnPlane.spawnList)
        {
            worldCenter.z = spawnGhost.transform.position.z; // 중앙점의 깊이 조정

            // 오브젝트의 스크린 위치 계산
            Vector3 objectScreenPosition = Camera.current.WorldToScreenPoint(spawnGhost.transform.position);

            // 감지 영역 내에 오브젝트가 있는지 확인
            if (Vector3.Distance(worldCenter, spawnGhost.transform.position) < 2.0f && Mathf.Abs(objectScreenPosition.x - screenCenter.x) < screenWidth / 2 && Mathf.Abs(objectScreenPosition.y - screenCenter.y) < screenHeight / 2)
            {
                if(panel == spawnGhost.GetComponent<Enemy>().part)
                {
                    if (arPlaceOnPlane.spawnList.Remove(spawnGhost))
                    {
                        TriggerCameraEffect();
                    }
                    Destroy(spawnGhost.gameObject);
                    return true;
                }
            }
        }
        return false;
    }
    public void Attacker()
    {
        int panel = arCameraFilter.PanelState;
        if(panel == 1 && arObjectClickHandler.Getexocism1() > 0)
        {
            if(DetectGhost(panel))
            {
                arObjectClickHandler.Setexocism1(-1);
            }
        }
        else if (panel == 2 && arObjectClickHandler.Getexocism2() > 0)
        {
            if (DetectGhost(panel))
            {
                arObjectClickHandler.Setexocism2(-1);
            }
        }
        else if (panel == 3 && arObjectClickHandler.Getexocism3() > 0)
        {
            if (DetectGhost(panel))
            {
                arObjectClickHandler.Setexocism3(-1);
            }
        }
    }
}
