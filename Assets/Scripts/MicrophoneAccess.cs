using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
public class MicrophoneAccess : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
