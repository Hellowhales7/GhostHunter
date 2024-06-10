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
    public CameraFilter cameraFilter;
    public GameObject placeObject;
    public TextMeshProUGUI DebugText;
    GameObject spawnObject;

    public GameObject warning;
    public List<GameObject> spawnList = new List<GameObject>();
    public List<GameObject> ObjectList = new List<GameObject>();
    public GameObject[] ItemList = new GameObject[3];
    public List<GameObject> spawnItemList= new List<GameObject>();
    public ARSceneManager sceneManager;
    public float detectionWidth = 0.1f; // �׸� �ݰ��� �� (ȭ�� �ʺ��� ����)
    public float detectionHeight = 0.1f; // �׸� �ݰ��� ���� (ȭ�� ������ ����)
    public Color rectangleColor = Color.red;  // �׸� ����             

    public ARPlaneManager planeManager; // AR Plane Manager
    public float spawnInterval = 10f; // ������Ʈ ���� ���� (��)
    private Dictionary<ARPlane, GameObject> placedObjects = new Dictionary<ARPlane, GameObject>(); // �� ��鿡 ��ġ�� ������Ʈ ����

    public GameObject objectPrefab;
    public float distanceBehind = 100.0f;
    private ARSessionOrigin arSessionOrigin;
    void Start()
    {
        if (planeManager == null)
        {
            planeManager = FindObjectOfType<ARPlaneManager>();
        }
        InvokeRepeating("PlaceObjectOnRandomPlane", spawnInterval, spawnInterval);

        //######
        //arSessionOrigin = FindObjectOfType<ARSessionOrigin>();

        //if (arSessionOrigin != null)
        //{
        //    AR ī�޶��� ��ġ�� �����ɴϴ�.
        //    Transform arCameraTransform = arSessionOrigin.camera.transform;

        //    ī�޶��� ���� �������� 5���� ��ġ�� ����մϴ�.
        //   Vector3 spawnPosition = arCameraTransform.position - arCameraTransform.forward * distanceBehind;

        //    ������Ʈ�� �ش� ��ġ�� �����մϴ�.
        //    Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        //}
        //else
        //{
        //    Debug.LogError("AR Session Origin�� ���� �����ϴ�.");
        //}
        // OnGUI();
    }

    // Update is called once per frame
    void Update()
    {
        DebugText.text = spawnList.Count.ToString();
        if (spawnList.Count > 5)
        {
            warning.SetActive(true);
        }
        else
            warning.SetActive(false);
        if (spawnList.Count > 7)
            sceneManager.GoToPreistDefeat();

       // PlaceObjectByTouch();
        DetectGhost();
    }
    void PlaceObjectOnRandomPlane()
    {
        if (spawnList.Count >= 9)
            return;
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

        // ����� ũ�⸦ �����ɴϴ�.
        Bounds planeBounds = selectedPlane.GetComponent<Renderer>().bounds;

        // ����� ���� ������ ���� ��ġ�� ����մϴ�.
        float randomX = Random.Range(planeBounds.min.x, planeBounds.max.x);
        float randomZ = Random.Range(planeBounds.min.z, planeBounds.max.z);
        Vector3 randomPosition = new Vector3(randomX, selectedPlane.transform.position.y, randomZ);

        Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0); // ������ ȸ��
        int index = Random.Range(0, ObjectList.Count);
        GameObject placedObject = Instantiate(ObjectList[index], randomPosition, rotation);

        if (cameraFilter.PanelState == placedObject.GetComponent<Enemy>().part)
            placedObject.SetActive(true);
        else
            placedObject.SetActive(false);

        //placedObjects.Add(selectedPlane, placedObject);
        spawnList.Add(placedObject);

        randomIndex = Random.Range(0, availablePlanes.Count);
        selectedPlane = availablePlanes[randomIndex];

        planeBounds = selectedPlane.GetComponent<Renderer>().bounds;

        // ����� ���� ������ ���� ��ġ�� ����մϴ�.
        randomX = Random.Range(planeBounds.min.x, planeBounds.max.x);
        randomZ = Random.Range(planeBounds.min.z, planeBounds.max.z);
        randomPosition = new Vector3(randomX, selectedPlane.transform.position.y, randomZ);

        rotation = Quaternion.Euler(0, Random.Range(0, 360), 0); // ������ ȸ��
        GameObject placedItem = Instantiate(ItemList[placedObject.GetComponent<Enemy>().part - 1], randomPosition, rotation);

        if (cameraFilter.PanelState == 0)
            placedItem.SetActive(true);
        else
            placedItem.SetActive(false);
        //placedObjects.Add(selectedPlane, placedItem);
        spawnItemList.Add(placedItem);


    }
    //private void PlaceObjectByTouch() // ��ġ�� ����
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);
    //        List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //        if (arRaycaster.Raycast(touch.position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
    //        {
    //            Pose hitPose = hits[0].pose;
    //            Vector3 cameraPos = Camera.current.transform.position;
    //            Vector3 lookAtDirection = (cameraPos - transform.position).normalized;

    //            if (!spawnObject)
    //            {

    //                spawnObject = Instantiate(placeObject, hitPose.position, hitPose.rotation * Quaternion.Euler(0,180,0));//Quaternion.LookRotation(lookAtDirection));
    //            }
    //            else
    //            {
    //                spawnObject.transform.position = hitPose.position;
    //                spawnObject.transform.rotation = hitPose.rotation;//Quaternion.LookRotation(lookAtDirection);
    //            }
    //        }
    //    }
    //}
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
                Temp.CountDown();
                // �߾� �׸� �ݰ� ���� ���� �� ���ϴ� ���� ����
            }
        }
    }
    //private void OnGUI()
    //{
    //    // ȭ�� �ʺ�� ����
    //    float screenWidth = Screen.width;
    //    float screenHeight = Screen.height;

    //    // �׸��� ���� �ʺ�� ����
    //    float rectWidth = screenWidth * detectionWidth;
    //    float rectHeight = screenHeight * detectionHeight;

    //    // �׸��� �»�� ��ġ ���
    //    float rectX = (screenWidth - rectWidth) / 2;
    //    float rectY = (screenHeight - rectHeight) / 2;

    //    // ���� GUI ������ �����մϴ�.
    //    Color oldColor = GUI.color;

    //    // �׸� ������ �����մϴ�.
    //    GUI.color = rectangleColor;

    //    // �׸� �׸��ϴ�.
    //    GUI.DrawTexture(new Rect(rectX, rectY, rectWidth, rectHeight), Texture2D.whiteTexture);

    //    // GUI ������ ������� ���������ϴ�.
    //    GUI.color = oldColor;
    //}
    //private bool UpdateCenterObject() // ����� ����
    //{
    //    Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
    //    //current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
    //    Debug.Log(screenCenter);
    //    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    //    arRaycaster.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

    //    if (hits.Count > 0)
    //    {
    //        Vector3 cameraPos = Camera.current.transform.position;
    //        Vector3 lookAtDirection = (cameraPos -transform.position).normalized;

    //        Pose placementPose = hits[0].pose;

    //        spawnObject = Instantiate(placeObject, placementPose.position, Quaternion.LookRotation(lookAtDirection));
    //        spawnObject.SetActive(true);
    //        return true;
    //        //placeObject.SetActive(true);
    //        //placeObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
    //    }
    //    return false;
    //}

}
