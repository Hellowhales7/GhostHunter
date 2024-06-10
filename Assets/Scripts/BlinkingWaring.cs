using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BlinkingWaring : MonoBehaviour
{
    public float blinkSpeed = 1.0f;  // 깜박임 속도 조절
    private float targetAlpha = 0.0f;  // 목표 알파 값
    private bool isFadingOut = true;  // 현재 페이드 아웃 중인지 여부

    public TextMeshProUGUI warning;
    void Update()
    {
        float alphaChange = blinkSpeed * Time.deltaTime;  // 알파 값 변경 속도
        Color currentColor = warning.color;
        float newAlpha;

        if (isFadingOut)
        {
            newAlpha = currentColor.a - alphaChange;  // 페이드 아웃
            if (newAlpha <= 0.0f)
            {
                newAlpha = 0.0f;
                isFadingOut = false;  // 방향 전환
            }
        }
        else
        {
            newAlpha = currentColor.a + alphaChange;  // 페이드 인
            if (newAlpha >= 1.0f)
            {
                newAlpha = 1.0f;
                isFadingOut = true;  // 방향 전환
            }
        }

        warning.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);  // 새 투명도 적용
    }
}
