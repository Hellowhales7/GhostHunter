using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bunnyAtack : MonoBehaviour
{
    private Animator animator;
    public Camera mainCamera; // 메인 카메라
    public float attackDistance = 5.0f; // 공격을 실행할 거리
    public float cameraSpeed = 2.0f; // 카메라 이동 속도
    public float cameraYOffset = -1.0f; // 카메라의 목표 y축 오프셋

    public AudioClip runningSound; // 달리는 소리 오디오 클립
    public AudioClip attackSound;
    private AudioSource audioSource; // 오디오 소스
    private enum State { Chase, Attack, Idle }
    private State currentState = State.Chase;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        // 메인 카메라를 참조
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Chase:
                Chase();
                break;
            case State.Attack:
                // Attack 상태에서는 별도 로직이 필요 없으므로 Idle로 넘어갑니다.
                break;
            case State.Idle:
                // Idle 상태에서는 별도 로직이 필요 없습니다.
                break;
        }
    }

    void Chase()
    {
        // 카메라와 캐릭터 사이의 거리 계산
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + cameraYOffset, transform.position.z);
        float distance = Vector3.Distance(mainCamera.transform.position, targetPosition);

        if (!audioSource.isPlaying)
        {
            audioSource.clip = runningSound;
            audioSource.loop = true;
            audioSource.Play();
        }

        // 카메라가 캐릭터로 다가가도록 이동
        if (distance > attackDistance)
        {
            Vector3 direction = (targetPosition - mainCamera.transform.position).normalized;
            mainCamera.transform.position += direction * cameraSpeed * Time.deltaTime;
        }
        else
        {

            Attack();
        }
    }

    void Attack()
    {
        currentState = State.Attack;
        animator.SetBool("Attack", true);

        audioSource.Stop();
        StartCoroutine(PlayAttackSoundWithDelay(1.0f));

        StartCoroutine(ResetAttackTrigger());
    }
    private IEnumerator PlayAttackSoundWithDelay(float delay)
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(delay);

        // 공격 소리 재생
        audioSource.clip = attackSound;
        audioSource.loop = false; // 공격 소리는 반복되지 않도록 설정
        audioSource.Play();
    }
    System.Collections.IEnumerator ResetAttackTrigger()
    {
        // Attack 애니메이션 길이만큼 대기 (예시로 1초 설정, 실제 애니메이션 길이에 맞춰 수정 필요)
        yield return new WaitForSeconds(2f);
        animator.SetBool("Attack", false);

        SceneManager.LoadScene(2);
        //currentState = State.Idle; 
    }
}
