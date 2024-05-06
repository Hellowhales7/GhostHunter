using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehindUser : MonoBehaviour
{
    public GameObject audioSourceObject; // ����� �ҽ� ������Ʈ
    public Camera arCamera; // AR ī�޶�
    private AudioSource audioSource; // ����� �ҽ� ������Ʈ
    private float playbackInterval = 10.0f; // ��� ���� (��)
    private float nextPlaybackTime; // ���� ��� �ð�

    void Start()
    {
        // ����� �ҽ� ������Ʈ ã��
        audioSource = audioSourceObject.GetComponent<AudioSource>();

        // �ʱ� ���� ��� �ð� ����
        nextPlaybackTime = Time.time + playbackInterval;

        // �ʱ� ��ġ ����
        PositionAudioSource();
    }

    void Update()
    {
        // ����ڰ� �����̸� ����� �ҽ� ��ġ ������Ʈ
        PositionAudioSource();

        // ���� �ð��� ���� ��� �ð����� ũ�ų� ���ٸ� ����� ���
        if (Time.time >= nextPlaybackTime)
        {
            audioSource.Play();  // ����� ���
            nextPlaybackTime += playbackInterval;  // ���� ��� �ð� ������Ʈ
        }
    }

    private void PositionAudioSource()
    {
        // ī�޶� ���ʿ� ����� �ҽ� ��ġ
        Vector3 behindPosition = arCamera.transform.position - arCamera.transform.forward * 5; // ī�޶� �� 2���� ��ġ
        audioSourceObject.transform.position = behindPosition;
    }
}
