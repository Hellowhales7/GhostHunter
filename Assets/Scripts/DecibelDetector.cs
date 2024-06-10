using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DecibelDetector : MonoBehaviour
{
    public float threshold = 1.5f;  // ���ú� �Ӱ谪 ����
    private AudioSource audioSource;
    private const int sampleWindowSize = 128;  // �м��� ���� ũ��
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

        // ����ũ ����, loop ����, ���̿� ���� ����Ʈ ����
        audioSource.clip = Microphone.Start(null, true, 1, 44100);
        audioSource.loop = true;
        // ����ũ�� �����͸� �����ϱ� ������ ������ ���
        while (!(Microphone.GetPosition(null) > 0)) { }
        //audioSource.Play();
    }

    void Update()
    {
        // ������ ����ũ �Է� �����͸� �м�
        float[] waveData = new float[sampleWindowSize];
        int micPosition = Microphone.GetPosition(null) - (sampleWindowSize + 1);  // ����ũ ��ġ ��������
        if (micPosition < 0) return;

        audioSource.clip.GetData(waveData, micPosition);

        // ��� ���� �� ���
        float levelMax = 0;
        for (int i = 0; i < sampleWindowSize; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }

        // ���ú��� ��ȯ
        float level = Mathf.Sqrt(Mathf.Sqrt(levelMax));

        // �Ӱ谪 Ȯ��
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
