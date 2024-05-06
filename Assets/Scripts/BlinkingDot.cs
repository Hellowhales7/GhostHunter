using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlinkingDot : MonoBehaviour
{
    public Image dotImage;  // �����̴� �̹���
    public float blinkSpeed = 1.0f;  // ������ �ӵ� ����
    private float targetAlpha = 0.0f;  // ��ǥ ���� ��
    private bool isFadingOut = true;  // ���� ���̵� �ƿ� ������ ����

    void Update()
    {
        float alphaChange = blinkSpeed * Time.deltaTime;  // ���� �� ���� �ӵ�
        Color currentColor = dotImage.color;
        float newAlpha;

        if (isFadingOut)
        {
            newAlpha = currentColor.a - alphaChange;  // ���̵� �ƿ�
            if (newAlpha <= 0.0f)
            {
                newAlpha = 0.0f;
                isFadingOut = false;  // ���� ��ȯ
            }
        }
        else
        {
            newAlpha = currentColor.a + alphaChange;  // ���̵� ��
            if (newAlpha >= 1.0f)
            {
                newAlpha = 1.0f;
                isFadingOut = true;  // ���� ��ȯ
            }
        }

        dotImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);  // �� ���� ����
    }
}
