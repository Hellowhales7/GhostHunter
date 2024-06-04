using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PriestEnding : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer = 3;
    public AudioClip soundClip;  // ����� ���� Ŭ��
    private AudioSource audioSource;
    int count = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        // Ÿ�̸Ӱ� 0 ���ϰ� �Ǹ� ���� ���
        if (timer <= 0)
        {
            if(count  ==1)
            {
                SceneManager.LoadScene("Defeated", LoadSceneMode.Single);
            }
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
            audioSource.Play();
            gameObject.transform.localPosition = new Vector3(0.0f, 3.0f, -9.0f);
            timer = 3;
            count++;
        }
    }
}
