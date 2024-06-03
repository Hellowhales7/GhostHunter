using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
public class ARObjectClickHandler : MonoBehaviour
{
    int exocism1 = 0;
    int exocism2 = 0;
    int exocism3 = 0;

    public ARRaycastManager _arRaycastManager;
    public TextMeshProUGUI item1;
    public TextMeshProUGUI item2;
    public TextMeshProUGUI item3;
    private static List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    void Start()
    {
        //_arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // 스크린 터치 위치에서 Raycast를 실행
                if (_arRaycastManager.Raycast(touch.position, _hits, TrackableType.PlaneWithinPolygon))
                {
                    // Raycast 결과 중 첫 번째 히트
                    var hitPose = _hits[0].pose;

                    // 히트된 위치에 있는 오브젝트 찾기
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hitObject;
                    if (Physics.Raycast(ray, out hitObject))
                    {
                        Item arObject = hitObject.transform.GetComponent<Item>();
                        if (arObject != null)
                        {
                            if (arObject.part == 1)
                            {
                                exocism1++;
                                item1.text = exocism1.ToString();
                            }
                            else if (arObject.part == 2)
                            {
                                exocism2++;
                                item2.text = exocism2.ToString();
                            }
                            else if (arObject.part == 3)
                            {
                                exocism3++;
                                item3.text = exocism3.ToString();
                            }
                                arObject.OnObjectTouched();
                        }
                    }
                }
            }
        }
    }
}
