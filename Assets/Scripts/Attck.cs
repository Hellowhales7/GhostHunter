using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Attck : MonoBehaviour
{
    // Start is called before the first frame update
    public ARObjectClickHandler arObjectClickHandler;
    public ARPlaceOnPlane arPlaceOnPlane;
    public CameraFilter arCameraFilter;
    public float detectionWidth = 0.5f; // �׸� �ݰ��� �� (ȭ�� �ʺ��� ����)
    public float detectionHeight = 0.5f; // �׸� �ݰ��� ���� (ȭ�� ������ ����)

    public AudioClip cameraShutterSound;  // Inspector���� �Ҵ��� ���� Ŭ��
    private AudioSource audioSource;
    public GameObject flashGameObj;
    public Image flashImage; // ȭ�� ����� ȿ���� ���� �̹���
    public float flashDuration = 0.5f; // ȭ�� ����� ȿ�� ���� �ð�
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = cameraShutterSound;
        if (flashImage != null)
        {
            flashImage.color = new Color(1, 1, 1, 0); // �ʱ� ������ �����ϰ� ����
        }
    }
    public void TriggerCameraEffect()
    {
        if (cameraShutterSound != null)
        {
            audioSource.PlayOneShot(cameraShutterSound);
        }

        if (flashImage != null)
        {
            StartCoroutine(FlashEffect());
        }
    }
    private IEnumerator FlashEffect()
    {
        // ȭ�� ����� ȿ��
        flashGameObj.SetActive(true);
        flashImage.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(flashDuration / 2);

        // ȭ�� ������� ���ƿ�
        flashImage.color = new Color(1, 1, 1, 0);
        flashGameObj.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private bool DetectGhost(int panel)
    {

        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 worldCenter = Camera.current.ScreenToWorldPoint(screenCenter);

        // ���� ���� ����
        float screenWidth = Screen.width * detectionWidth;
        float screenHeight = Screen.height * detectionHeight;


        foreach (var spawnGhost in arPlaceOnPlane.spawnList)
        {
            worldCenter.z = spawnGhost.transform.position.z; // �߾����� ���� ����

            // ������Ʈ�� ��ũ�� ��ġ ���
            Vector3 objectScreenPosition = Camera.current.WorldToScreenPoint(spawnGhost.transform.position);

            // ���� ���� ���� ������Ʈ�� �ִ��� Ȯ��
            if (Vector3.Distance(worldCenter, spawnGhost.transform.position) < 2.0f && Mathf.Abs(objectScreenPosition.x - screenCenter.x) < screenWidth / 2 && Mathf.Abs(objectScreenPosition.y - screenCenter.y) < screenHeight / 2)
            {
                if(panel == spawnGhost.GetComponent<Enemy>().part)
                {
                    if (arPlaceOnPlane.spawnList.Remove(spawnGhost))
                    {
                        TriggerCameraEffect();
                    }
                    Destroy(spawnGhost.gameObject);
                    return true;
                }
            }
        }
        return false;
    }
    public void Attacker()
    {
        int panel = arCameraFilter.PanelState;
        if(panel == 1 && arObjectClickHandler.Getexocism1() > 0)
        {
            if(DetectGhost(panel))
            {
                arObjectClickHandler.Setexocism1(-1);
            }
        }
        else if (panel == 2 && arObjectClickHandler.Getexocism2() > 0)
        {
            if (DetectGhost(panel))
            {
                arObjectClickHandler.Setexocism2(-1);
            }
        }
        else if (panel == 3 && arObjectClickHandler.Getexocism3() > 0)
        {
            if (DetectGhost(panel))
            {
                arObjectClickHandler.Setexocism3(-1);
            }
        }
    }
}
