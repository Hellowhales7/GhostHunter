using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class ARObjectClickHandler : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private int Exocism1 = 0;
    private int Exocism2 = 0;
    private int Exocism3 = 0;

    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        // 터치 감지
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // UI 클릭이면 무시
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return;
                }

                // 터치 위치에서 Ray 생성
                if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    var hitPose = hits[0].pose;

                    // RaycastHit을 이용해 오브젝트 획득 처리
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.GetComponent<Item>().part == 1)
                        {
                            Exocism1++;
                        }
                        else if(hit.collider.gameObject.GetComponent<Item>().part == 2)
                        {
                            Exocism2++;
                        }
                        else if(hit.collider.gameObject.GetComponent<Item>().part == 3)
                        {
                            Exocism3++;
                        }
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}
