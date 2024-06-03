using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicSoundPlayer : MonoBehaviour
{
    public AudioClip soundClip;  // ����� ���� Ŭ��
    public float interval = 5.0f;  // ���带 �ݺ��� ���� (�� ����)

    private AudioSource audioSource;
    private float timer;

    void Start()
    {
        // AudioSource ������Ʈ �������� �Ǵ� �߰��ϱ�
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // AudioSource ����
        audioSource.clip = soundClip;
        audioSource.spatialBlend = 1.0f;  // 3D ����� ����
        audioSource.loop = false;  // �ݺ� ��� ��Ȱ��ȭ
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;  // �Ÿ� ���� ����
        audioSource.minDistance = 1.0f;  // �ּ� �Ÿ� ����
        audioSource.maxDistance = 50.0f;  // �ִ� �Ÿ� ����

        timer = interval;  // Ÿ�̸� �ʱ�ȭ
    }

    void Update()
    {
        // Ÿ�̸� ����
        timer -= Time.deltaTime;

        // Ÿ�̸Ӱ� 0 ���ϰ� �Ǹ� ���� ���
        if (timer <= 0)
        {
            audioSource.Play();
            timer = interval;  // Ÿ�̸� ����
        }
    }
}
