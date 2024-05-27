using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 45f; // �ʴ� ȸ�� ����
    private float currentRotationAngle = 0f; // ���� ȸ�� ����
    public float moveSpeed = 0.5f; // �ʴ� �̵� �ӵ�
    public Camera arCamera;
    public bool detect = false;
    public int part;
    public CameraFilter cameraFilter;
    int Counter = 2;
    float CounterTerm = 3.0f;
    float CounterTimer = 0.0f;

    Vector3 initScale;
    public float visibilityDistance = 1.0f; // ���̴� �Ÿ�

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
        // AR ī�޶��� ��ġ�� ���� ������Ʈ�� ��ġ ������ ���͸� ����մϴ�.
        
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

            // �ε巴�� �̵��ϱ� ���� directionToCamera�� �̵� �ӵ��� delta Ÿ���� ���մϴ�.
            Vector3 moveAmount = directionToCamera * moveSpeed * Time.deltaTime;

            // ���� ������Ʈ�� ��ġ�� �̵���ŵ�ϴ�.
            transform.position += moveAmount;

        }

    }
}
