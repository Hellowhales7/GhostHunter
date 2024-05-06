using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CameraTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;  // UI에서 타이머 텍스트를 표시할 Text 컴포넌트
    private float startTime;  // 타이머가 시작된 시간
    private bool timerActive = false;  // 타이머의 활성 상태

    void Start()
    {
        // 타이머 시작
        StartTimer();
    }

    void Update()
    {
        if (timerActive)
        {
            float t = Time.time - startTime;  // 경과 시간 계산
            string minutes = ((int)t / 60).ToString("00");
            string seconds = ((int)t % 60).ToString("00");
            string milliseconds = ((int)(t * 100) % 100).ToString("00");

            timerText.text = minutes + ":" + seconds + ":" + milliseconds;  // 텍스트 업데이트
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;  // 현재 시간으로 시작 시간 설정
        timerActive = true;  // 타이머를 활성화
    }

    public void StopTimer()
    {
        timerActive = false;  // 타이머를 비활성화
    }

    public void ResetTimer()
    {
        StartTimer();  // 타이머 재시작
    }
}
