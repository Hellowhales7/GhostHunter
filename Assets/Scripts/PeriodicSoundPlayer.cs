using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicSoundPlayer : MonoBehaviour
{
    public AudioClip soundClip;  // 재생할 사운드 클립
    public float interval = 5.0f;  // 사운드를 반복할 간격 (초 단위)

    private AudioSource audioSource;
    private float timer;

    void Start()
    {
        // AudioSource 컴포넌트 가져오기 또는 추가하기
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // AudioSource 설정
        audioSource.clip = soundClip;
        audioSource.spatialBlend = 1.0f;  // 3D 사운드로 설정
        audioSource.loop = false;  // 반복 재생 비활성화
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;  // 거리 감쇠 설정
        audioSource.minDistance = 1.0f;  // 최소 거리 설정
        audioSource.maxDistance = 50.0f;  // 최대 거리 설정

        timer = interval;  // 타이머 초기화
    }

    void Update()
    {
        // 타이머 갱신
        timer -= Time.deltaTime;

        // 타이머가 0 이하가 되면 사운드 재생
        if (timer <= 0)
        {
            audioSource.Play();
            timer = interval;  // 타이머 리셋
        }
    }
}
