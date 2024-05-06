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
        // AR ī�޶��� ��ġ�� ���� ������Ʈ�� ��ġ ������ ���͸� ����մϴ�.
        CounterTimer += Time.deltaTime;
        if (Counter < 0)
        {
            Vector3 cameraPosition = Camera.current.transform.position;
            Vector3 directionToCamera = (cameraPosition - transform.position).normalized;

            // �ε巴�� �̵��ϱ� ���� directionToCamera�� �̵� �ӵ��� delta Ÿ���� ���մϴ�.
            Vector3 moveAmount = directionToCamera * moveSpeed * Time.deltaTime;

            // ���� ������Ʈ�� ��ġ�� �̵���ŵ�ϴ�.
            transform.position += moveAmount;
            //Quaternion currentRotation = transform.rotation;

            //// ȸ�� ������ delta Ÿ�ӿ� ���� ������Ʈ�մϴ�.
            //currentRotationAngle += rotationSpeed * Time.deltaTime;

            //// y���� �߽����� �ε巴�� ȸ����Ű�� ���ʹϾ��� ���մϴ�.
            //Quaternion targetRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            //// Lerp�� ����Ͽ� �ε巴�� ���ʹϾ��� �����մϴ�.
            //Quaternion newRotation = Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime * 5f); // 5f�� ������ �ӵ��� ��Ÿ���ϴ�. ���� ����

            //// ���ο� ȸ�� ���� ���� ������Ʈ�� ȸ�� ������ �����մϴ�.
            //transform.rotation = newRotation;
        }

    }
}
