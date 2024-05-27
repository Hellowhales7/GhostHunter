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
        // ��ġ ����
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // UI Ŭ���̸� ����
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return;
                }

                // ��ġ ��ġ���� Ray ����
                if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    var hitPose = hits[0].pose;

                    // RaycastHit�� �̿��� ������Ʈ ȹ�� ó��
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
