using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PriestEnding : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer = 3;
    public AudioClip soundClip;  // 재생할 사운드 클립
    private AudioSource audioSource;
    int count = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        // 타이머가 0 이하가 되면 사운드 재생
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

            // AudioSource 설정
            audioSource.clip = soundClip;
            audioSource.spatialBlend = 1.0f;  // 3D 사운드로 설정
            audioSource.loop = false;  // 반복 재생 비활성화
            audioSource.rolloffMode = AudioRolloffMode.Logarithmic;  // 거리 감쇠 설정
            audioSource.minDistance = 1.0f;  // 최소 거리 설정
            audioSource.maxDistance = 50.0f;  // 최대 거리 설정
            audioSource.Play();
            gameObject.transform.localPosition = new Vector3(0.0f, 3.0f, -9.0f);
            timer = 3;
            count++;
        }
    }
}
