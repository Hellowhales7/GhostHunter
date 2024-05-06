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
    private Camera arCamera;
    public bool detect = false;
    int Counter = 3;
    float CounterTerm = 3.0f;
    float CounterTimer = 0.0f;
    void Start()
    {
        //arCamera = FindObjectOfType<ARCamera>().GetComponent<Camera>();
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
        CounterTimer += Time.deltaTime;
        if (Counter < 0)
        {
            Vector3 cameraPosition = Camera.current.transform.position;
            Vector3 directionToCamera = (cameraPosition - transform.position).normalized;

            // 부드럽게 이동하기 위해 directionToCamera에 이동 속도와 delta 타임을 곱합니다.
            Vector3 moveAmount = directionToCamera * moveSpeed * Time.deltaTime;

            // 게임 오브젝트의 위치를 이동시킵니다.
            transform.position += moveAmount;
            //Quaternion currentRotation = transform.rotation;

            //// 회전 각도를 delta 타임에 따라 업데이트합니다.
            //currentRotationAngle += rotationSpeed * Time.deltaTime;

            //// y축을 중심으로 부드럽게 회전시키는 쿼터니언을 구합니다.
            //Quaternion targetRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            //// Lerp를 사용하여 부드럽게 쿼터니언을 보간합니다.
            //Quaternion newRotation = Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime * 5f); // 5f는 보간의 속도를 나타냅니다. 조절 가능

            //// 새로운 회전 값을 게임 오브젝트의 회전 값으로 설정합니다.
            //transform.rotation = newRotation;
        }

    }
}
