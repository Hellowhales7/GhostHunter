using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningSound : MonoBehaviour
{
    public AudioClip runningClip;  // Inspector���� �Ҵ��� ���� Ŭ��
    private AudioSource audioSource;
    public Transform player;       // �÷��̾��� Transform
    public float speed = 10.0f;     // ������Ʈ�� �÷��̾�� �޷����� �ӵ�

    public float destroyDistance = 1.0f;
    void Start()
    {
        // AudioSource ������Ʈ�� �߰��ϰ� ����
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = runningClip;
        audioSource.spatialBlend = 1.0f; // 3D ���� Ȱ��ȭ
        audioSource.minDistance = 1.0f;  // �ּ� �Ÿ� ����
        audioSource.maxDistance = 50.0f; // �ִ� �Ÿ� ����
        audioSource.loop = true;         // ���� ����
        audioSource.Play();              // ���� ��� ����
    }

    void Update()
    {
        // ������Ʈ�� �÷��̾ ���� �̵�
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
