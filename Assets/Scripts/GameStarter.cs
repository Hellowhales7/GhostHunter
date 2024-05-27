using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    public TextMeshProUGUI PlaneCount;
    public ARPlaneManager planeManager;
    public GameObject GhostSpawner;
    public GameObject StartButton;
    public GameObject NeedMorePlaneText;
    public int minPlane;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        int PlaneCnt = planeManager.trackables.count;
        PlaneCount.text = PlaneCnt.ToString();
        if(PlaneCnt > minPlane)
        {
            StartButton.SetActive(true);
            if (NeedMorePlaneText != null)
            {
                Destroy(NeedMorePlaneText);
            }
        }
    }
    public void GameStart()
    {
        GhostSpawner.SetActive(true);
        Destroy(StartButton);
        
    }
}
