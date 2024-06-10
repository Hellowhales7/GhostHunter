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
    public float detectionWidth = 0.1f; // 네모 반경의 폭 (화면 너비의 비율)
    public float detectionHeight = 0.1f; // 네모 반경의 높이 (화면 높이의 비율)
    public Color rectangleColor = Color.red;  // 네모 색상             

    public ARPlaneManager planeManager; // AR Plane Manager
    public float spawnInterval = 10f; // 오브젝트 생성 간격 (초)
    private Dictionary<ARPlane, GameObject> placedObjects = new Dictionary<ARPlane, GameObject>(); // 각 평면에 배치된 오브젝트 추적

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
        //    AR 카메라의 위치를 가져옵니다.
        //    Transform arCameraTransform = arSessionOrigin.camera.transform;

        //    카메라의 뒤쪽 방향으로 5미터 위치를 계산합니다.
        //   Vector3 spawnPosition = arCameraTransform.position - arCameraTransform.forward * distanceBehind;

        //    오브젝트를 해당 위치에 생성합니다.
        //    Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        //}
        //else
        //{
        //    Debug.LogError("AR Session Origin이 씬에 없습니다.");
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
        // 사용 가능한 평면 목록 가져오기
        List<ARPlane> availablePlanes = new List<ARPlane>();
        foreach (ARPlane plane in planeManager.trackables)
        {
            if (!placedObjects.ContainsKey(plane))
            {
                availablePlanes.Add(plane);
            }
        }

        // 사용 가능한 평면이 있는지 확인
        if (availablePlanes.Count == 0)
        {
            Debug.Log("No planes available");
            return;
        }

        // 무작위 평면 선택
        int randomIndex = Random.Range(0, availablePlanes.Count);
        ARPlane selectedPlane = availablePlanes[randomIndex];

        // 평면의 크기를 가져옵니다.
        Bounds planeBounds = selectedPlane.GetComponent<Renderer>().bounds;

        // 평면의 범위 내에서 랜덤 위치를 계산합니다.
        float randomX = Random.Range(planeBounds.min.x, planeBounds.max.x);
        float randomZ = Random.Range(planeBounds.min.z, planeBounds.max.z);
        Vector3 randomPosition = new Vector3(randomX, selectedPlane.transform.position.y, randomZ);

        Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0); // 무작위 회전
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

        // 평면의 범위 내에서 랜덤 위치를 계산합니다.
        randomX = Random.Range(planeBounds.min.x, planeBounds.max.x);
        randomZ = Random.Range(planeBounds.min.z, planeBounds.max.z);
        randomPosition = new Vector3(randomX, selectedPlane.transform.position.y, randomZ);

        rotation = Quaternion.Euler(0, Random.Range(0, 360), 0); // 무작위 회전
        GameObject placedItem = Instantiate(ItemList[placedObject.GetComponent<Enemy>().part - 1], randomPosition, rotation);

        if (cameraFilter.PanelState == 0)
            placedItem.SetActive(true);
        else
            placedItem.SetActive(false);
        //placedObjects.Add(selectedPlane, placedItem);
        spawnItemList.Add(placedItem);


    }
    //private void PlaceObjectByTouch() // 터치로 생성
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
        worldCenter.z = spawnObject.transform.position.z; // 중앙점의 깊이 조정

        // 감지 영역 설정
        float screenWidth = Screen.width * detectionWidth;
        float screenHeight = Screen.height * detectionHeight;

        // 오브젝트의 스크린 위치 계산
        Vector3 objectScreenPosition = Camera.current.WorldToScreenPoint(spawnObject.transform.position);

        // 감지 영역 내에 오브젝트가 있는지 확인
        if (Vector3.Distance(worldCenter, spawnObject.transform.position) < 1.0f && Mathf.Abs(objectScreenPosition.x - screenCenter.x) < screenWidth / 2 && Mathf.Abs(objectScreenPosition.y - screenCenter.y) < screenHeight / 2)
        {
            DebugText.text = "Detect Ghost";
            Enemy Temp = spawnObject.GetComponent<Enemy>();
            Temp.detect = true;
            // 중앙 네모 반경 내에 있을 때 원하는 동작 실행
        }

        foreach (var spawnGhost in spawnList)
        {
            worldCenter.z = spawnGhost.transform.position.z; // 중앙점의 깊이 조정

            // 오브젝트의 스크린 위치 계산
            objectScreenPosition = Camera.current.WorldToScreenPoint(spawnGhost.transform.position);

            // 감지 영역 내에 오브젝트가 있는지 확인
            if (Vector3.Distance(worldCenter, spawnGhost.transform.position) < 1.0f && Mathf.Abs(objectScreenPosition.x - screenCenter.x) < screenWidth / 2 && Mathf.Abs(objectScreenPosition.y - screenCenter.y) < screenHeight / 2)
            {
                DebugText.text = "Detect Ghost : " + spawnGhost.name;
                Enemy Temp = spawnGhost.GetComponent<Enemy>();
                Temp.CountDown();
                // 중앙 네모 반경 내에 있을 때 원하는 동작 실행
            }
        }
    }
    //private void OnGUI()
    //{
    //    // 화면 너비와 높이
    //    float screenWidth = Screen.width;
    //    float screenHeight = Screen.height;

    //    // 네모의 실제 너비와 높이
    //    float rectWidth = screenWidth * detectionWidth;
    //    float rectHeight = screenHeight * detectionHeight;

    //    // 네모의 좌상단 위치 계산
    //    float rectX = (screenWidth - rectWidth) / 2;
    //    float rectY = (screenHeight - rectHeight) / 2;

    //    // 기존 GUI 색상을 저장합니다.
    //    Color oldColor = GUI.color;

    //    // 네모 색상을 설정합니다.
    //    GUI.color = rectangleColor;

    //    // 네모를 그립니다.
    //    GUI.DrawTexture(new Rect(rectX, rectY, rectWidth, rectHeight), Texture2D.whiteTexture);

    //    // GUI 색상을 원래대로 돌려놓습니다.
    //    GUI.color = oldColor;
    //}
    //private bool UpdateCenterObject() // 가운데에 생성
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
