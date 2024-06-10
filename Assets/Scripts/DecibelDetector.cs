using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DecibelDetector : MonoBehaviour
{
    public float threshold = 1.5f;  // 데시벨 임계값 설정
    private AudioSource audioSource;
    private const int sampleWindowSize = 128;  // 분석할 샘플 크기
    public Image mic;
    ARPlaceOnPlane ARPOP;

    void Start()
    {
        ARPOP = GetComponent<ARPlaceOnPlane>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 마이크 시작, loop 설정, 길이와 샘플 레이트 설정
        audioSource.clip = Microphone.Start(null, true, 1, 44100);
        audioSource.loop = true;
        // 마이크가 데이터를 축적하기 시작할 때까지 대기
        while (!(Microphone.GetPosition(null) > 0)) { }
        //audioSource.Play();
    }

    void Update()
    {
        // 현재의 마이크 입력 데이터를 분석
        float[] waveData = new float[sampleWindowSize];
        int micPosition = Microphone.GetPosition(null) - (sampleWindowSize + 1);  // 마이크 위치 가져오기
        if (micPosition < 0) return;

        audioSource.clip.GetData(waveData, micPosition);

        // 평균 제곱 값 계산
        float levelMax = 0;
        for (int i = 0; i < sampleWindowSize; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }

        // 데시벨로 변환
        float level = Mathf.Sqrt(Mathf.Sqrt(levelMax));

        // 임계값 확인
        if (level > threshold)
        {
            foreach (var spawnGhost in ARPOP.spawnList)
            {
                Enemy temp = spawnGhost.GetComponent<Enemy>();
                temp.CountDown();
            }
            mic.color = Color.red;
        }
        else
        {
            mic.color = new Color(1, 1, 1, 1);
        }
    }
}
