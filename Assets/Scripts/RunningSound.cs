using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningSound : MonoBehaviour
{
    public AudioClip runningClip;  // Inspector에서 할당할 사운드 클립
    private AudioSource audioSource;
    public Transform player;       // 플레이어의 Transform
    public float speed = 10.0f;     // 오브젝트가 플레이어에게 달려오는 속도

    public float destroyDistance = 1.0f;
    void Start()
    {
        // AudioSource 컴포넌트를 추가하고 설정
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = runningClip;
        audioSource.spatialBlend = 1.0f; // 3D 사운드 활성화
        audioSource.minDistance = 1.0f;  // 최소 거리 설정
        audioSource.maxDistance = 50.0f; // 최대 거리 설정
        audioSource.loop = true;         // 루프 설정
        audioSource.Play();              // 사운드 재생 시작
    }

    void Update()
    {
        // 오브젝트가 플레이어를 향해 이동
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
