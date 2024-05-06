using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

public class ARPlaceOnPlane : MonoBehaviour
{
    public ARRaycastManager arRaycaster;
    public GameObject placeObject;
    public TextMeshProUGUI DebugText;
    GameObject spawnObject;
    public List<GameObject> spawnList = new List<GameObject>();
    public List<GameObject> ObjectList = new List<GameObject>();
    float time = 10.0f;
    float timeset = 10.0f;
    float rotationspeed = 5f;

    public float detectionWidth = 0.1f; // �׸� �ݰ��� �� (ȭ�� �ʺ��� ����)
    public float detectionHeight = 0.1f; // �׸� �ݰ��� ���� (ȭ�� ������ ����)
                                         // Start is called before the first frame update

    public ARPlaneManager planeManager; // AR Plane Manager
    public float spawnInterval = 5f; // ������Ʈ ���� ���� (��)
    private Dictionary<ARPlane, GameObject> placedObjects = new Dictionary<ARPlane, GameObject>(); // �� ��鿡 ��ġ�� ������Ʈ ����
    void Start()
    {
        if (planeManager == null)
        {
            planeManager = FindObjectOfType<ARPlaneManager>();
        }
        InvokeRepeating("PlaceObjectOnRandomPlane", spawnInterval, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        PlaceObjectByTouch();
        DetectGhost();
    }
    void PlaceObjectOnRandomPlane()
    {
        // ��� ������ ��� ��� ��������
        List<ARPlane> availablePlanes = new List<ARPlane>();
        foreach (ARPlane plane in planeManager.trackables)
        {
            if (!placedObjects.ContainsKey(plane))
            {
                availablePlanes.Add(plane);
            }
        }

        // ��� ������ ����� �ִ��� Ȯ��
        if (availablePlanes.Count == 0)
        {
            Debug.Log("No planes available");
            return;
        }

        // ������ ��� ����
        int randomIndex = Random.Range(0, availablePlanes.Count);
        ARPlane selectedPlane = availablePlanes[randomIndex];

        // ���õ� ����� �߽ɿ� ������Ʈ ��ġ
        Vector3 position = selectedPlane.transform.position;
        Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0); // ������ ȸ��
        int index = Random.Range(0, ObjectList.Count);
        GameObject placedObject = Instantiate(ObjectList[index], position, rotation);
        placedObject.SetActive(true);
        placedObjects.Add(selectedPlane, placedObject);
        spawnList.Add(placedObject);
    }
    private void PlaceObjectByTouch() // ��ġ�� ����
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            if (arRaycaster.Raycast(touch.position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                Pose hitPose = hits[0].pose;
                Vector3 cameraPos = Camera.current.transform.position;
                Vector3 lookAtDirection = (cameraPos - transform.position).normalized;

                if (!spawnObject)
                {

                    spawnObject = Instantiate(placeObject, hitPose.position, hitPose.rotation * Quaternion.Euler(0,180,0));//Quaternion.LookRotation(lookAtDirection));
                }
                else
                {
                    spawnObject.transform.position = hitPose.position;
                    spawnObject.transform.rotation = hitPose.rotation;//Quaternion.LookRotation(lookAtDirection);
                }
            }
        }
    }
    private void DetectGhost()
    {

        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 worldCenter = Camera.current.ScreenToWorldPoint(screenCenter);
        worldCenter.z = spawnObject.transform.position.z; // �߾����� ���� ����

        // ���� ���� ����
        float screenWidth = Screen.width * detectionWidth;
        float screenHeight = Screen.height * detectionHeight;

        // ������Ʈ�� ��ũ�� ��ġ ���
        Vector3 objectScreenPosition = Camera.current.WorldToScreenPoint(spawnObject.transform.position);

        // ���� ���� ���� ������Ʈ�� �ִ��� Ȯ��
        if (Vector3.Distance(worldCenter, spawnObject.transform.position) < 1.0f && Mathf.Abs(objectScreenPosition.x - screenCenter.x) < screenWidth / 2 && Mathf.Abs(objectScreenPosition.y - screenCenter.y) < screenHeight / 2)
        {
            DebugText.text = "Detect Ghost";
            Enemy Temp = spawnObject.GetComponent<Enemy>();
            Temp.detect = true;
            // �߾� �׸� �ݰ� ���� ���� �� ���ϴ� ���� ����
        }

        foreach (var spawnGhost in spawnList)
        {
            worldCenter.z = spawnGhost.transform.position.z; // �߾����� ���� ����

            // ������Ʈ�� ��ũ�� ��ġ ���
            objectScreenPosition = Camera.current.WorldToScreenPoint(spawnGhost.transform.position);

            // ���� ���� ���� ������Ʈ�� �ִ��� Ȯ��
            if (Vector3.Distance(worldCenter, spawnGhost.transform.position) < 1.0f && Mathf.Abs(objectScreenPosition.x - screenCenter.x) < screenWidth / 2 && Mathf.Abs(objectScreenPosition.y - screenCenter.y) < screenHeight / 2)
            {
                DebugText.text = "Detect Ghost : " + spawnGhost.name;
                Enemy Temp = spawnGhost.GetComponent<Enemy>();
                Temp.detect = true;
                // �߾� �׸� �ݰ� ���� ���� �� ���ϴ� ���� ����
            }
        }
    }
    
    private bool UpdateCenterObject() // ����� ����
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        //current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        Debug.Log(screenCenter);
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycaster.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            Vector3 cameraPos = Camera.current.transform.position;
            Vector3 lookAtDirection = (cameraPos -transform.position).normalized;

            Pose placementPose = hits[0].pose;
            
            spawnObject = Instantiate(placeObject, placementPose.position, Quaternion.LookRotation(lookAtDirection));
            spawnObject.SetActive(true);
            return true;
            //placeObject.SetActive(true);
            //placeObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        return false;
    }

}
