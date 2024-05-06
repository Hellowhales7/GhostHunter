using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehindUser : MonoBehaviour
{
    public GameObject audioSourceObject; // 오디오 소스 오브젝트
    public Camera arCamera; // AR 카메라
    private AudioSource audioSource; // 오디오 소스 컴포넌트
    private float playbackInterval = 10.0f; // 재생 간격 (초)
    private float nextPlaybackTime; // 다음 재생 시간

    void Start()
    {
        // 오디오 소스 컴포넌트 찾기
        audioSource = audioSourceObject.GetComponent<AudioSource>();

        // 초기 다음 재생 시간 설정
        nextPlaybackTime = Time.time + playbackInterval;

        // 초기 위치 설정
        PositionAudioSource();
    }

    void Update()
    {
        // 사용자가 움직이면 오디오 소스 위치 업데이트
        PositionAudioSource();

        // 현재 시간이 다음 재생 시간보다 크거나 같다면 오디오 재생
        if (Time.time >= nextPlaybackTime)
        {
            audioSource.Play();  // 오디오 재생
            nextPlaybackTime += playbackInterval;  // 다음 재생 시간 업데이트
        }
    }

    private void PositionAudioSource()
    {
        // 카메라 뒤쪽에 오디오 소스 배치
        Vector3 behindPosition = arCamera.transform.position - arCamera.transform.forward * 5; // 카메라 뒤 2미터 위치
        audioSourceObject.transform.position = behindPosition;
    }
}
