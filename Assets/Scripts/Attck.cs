using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attck : MonoBehaviour
{
    // Start is called before the first frame update
    public ARObjectClickHandler arObjectClickHandler;
    public ARPlaceOnPlane arPlaceOnPlane;
    public CameraFilter arCameraFilter;
    public float detectionWidth = 0.5f; // 네모 반경의 폭 (화면 너비의 비율)
    public float detectionHeight = 0.5f; // 네모 반경의 높이 (화면 높이의 비율)
    void Start()
    {
        
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
                        arObjectClickHandler.Setexocism1(10);
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
