using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 45f; // 초당 회전 각도
    private float currentRotationAngle = 0f; // 현재 회전 각도
    public float moveSpeed = 0.5f; // 초당 이동 속도
    public Camera arCamera;
    public bool detect = false;
    public int part;
    public CameraFilter cameraFilter;
    int Counter = 2;
    float CounterTerm = 3.0f;
    float CounterTimer = 0.0f;

    Vector3 initScale;
    public float visibilityDistance = 1.0f; // 보이는 거리

    void Start()
    {
        initScale = transform.localScale;
    }
    public void CountDown()
    {
        if(CounterTimer > CounterTerm)
        {
            Counter--;
            CounterTimer = 0.0f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        // AR 카메라의 위치와 게임 오브젝트의 위치 사이의 벡터를 계산합니다.
        
        float distance = Vector3.Distance(arCamera.transform.position, gameObject.transform.position);

        if(distance >= visibilityDistance)
        {
            gameObject.transform.localScale = Vector3.zero;
        }
        else
        {
            gameObject.transform.localScale = initScale;
        }


        CounterTimer += Time.deltaTime;
        if (Counter < 0)
        {
            Vector3 cameraPosition = Camera.current.transform.position /*+ Vector3.down*/;
            Vector3 directionToCamera = (cameraPosition - transform.position).normalized;

            // 부드럽게 이동하기 위해 directionToCamera에 이동 속도와 delta 타임을 곱합니다.
            Vector3 moveAmount = directionToCamera * moveSpeed * Time.deltaTime;

            // 게임 오브젝트의 위치를 이동시킵니다.
            transform.position += moveAmount;

        }

    }
}
