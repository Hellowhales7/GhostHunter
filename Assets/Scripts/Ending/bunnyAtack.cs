using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bunnyAtack : MonoBehaviour
{
    private Animator animator;
    public Camera mainCamera; // ���� ī�޶�
    public float attackDistance = 5.0f; // ������ ������ �Ÿ�
    public float cameraSpeed = 2.0f; // ī�޶� �̵� �ӵ�
    public float cameraYOffset = -1.0f; // ī�޶��� ��ǥ y�� ������

    public AudioClip runningSound; // �޸��� �Ҹ� ����� Ŭ��
    public AudioClip attackSound;
    private AudioSource audioSource; // ����� �ҽ�
    private enum State { Chase, Attack, Idle }
    private State currentState = State.Chase;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        // ���� ī�޶� ����
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
                // Attack ���¿����� ���� ������ �ʿ� �����Ƿ� Idle�� �Ѿ�ϴ�.
                break;
            case State.Idle:
                // Idle ���¿����� ���� ������ �ʿ� �����ϴ�.
                break;
        }
    }

    void Chase()
    {
        // ī�޶�� ĳ���� ������ �Ÿ� ���
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + cameraYOffset, transform.position.z);
        float distance = Vector3.Distance(mainCamera.transform.position, targetPosition);

        if (!audioSource.isPlaying)
        {
            audioSource.clip = runningSound;
            audioSource.loop = true;
            audioSource.Play();
        }

        // ī�޶� ĳ���ͷ� �ٰ������� �̵�
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
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(delay);

        // ���� �Ҹ� ���
        audioSource.clip = attackSound;
        audioSource.loop = false; // ���� �Ҹ��� �ݺ����� �ʵ��� ����
        audioSource.Play();
    }
    System.Collections.IEnumerator ResetAttackTrigger()
    {
        // Attack �ִϸ��̼� ���̸�ŭ ��� (���÷� 1�� ����, ���� �ִϸ��̼� ���̿� ���� ���� �ʿ�)
        yield return new WaitForSeconds(2f);
        animator.SetBool("Attack", false);

        SceneManager.LoadScene(2);
        //currentState = State.Idle; 
    }
}
