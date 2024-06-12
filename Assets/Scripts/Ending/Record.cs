using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Record : MonoBehaviour
{
    public TextMeshProUGUI finalTimeText;

    void Start()
    {
        float t = PlayerPrefs.GetFloat("finalTime", 0);
        string minutes = ((int)t / 60).ToString("00");
        string seconds = ((int)t % 60).ToString("00");
        string milliseconds = ((int)(t * 100) % 100).ToString("00");

        finalTimeText.text = minutes + ":" + seconds + ":" + milliseconds;
    }
}
