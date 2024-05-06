using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CameraTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;  // UI���� Ÿ�̸� �ؽ�Ʈ�� ǥ���� Text ������Ʈ
    private float startTime;  // Ÿ�̸Ӱ� ���۵� �ð�
    private bool timerActive = false;  // Ÿ�̸��� Ȱ�� ����

    void Start()
    {
        // Ÿ�̸� ����
        StartTimer();
    }

    void Update()
    {
        if (timerActive)
        {
            float t = Time.time - startTime;  // ��� �ð� ���
            string minutes = ((int)t / 60).ToString("00");
            string seconds = ((int)t % 60).ToString("00");
            string milliseconds = ((int)(t * 100) % 100).ToString("00");

            timerText.text = minutes + ":" + seconds + ":" + milliseconds;  // �ؽ�Ʈ ������Ʈ
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;  // ���� �ð����� ���� �ð� ����
        timerActive = true;  // Ÿ�̸Ӹ� Ȱ��ȭ
    }

    public void StopTimer()
    {
        timerActive = false;  // Ÿ�̸Ӹ� ��Ȱ��ȭ
    }

    public void ResetTimer()
    {
        StartTimer();  // Ÿ�̸� �����
    }
}
