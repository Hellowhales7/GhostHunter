using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CameraFilter : MonoBehaviour
{
    // Start is called before the first frame update
    public Image PanelImage;
    public int PanelState = 0;
    public ARPlaceOnPlane ARP;
    public TextMeshProUGUI filterDebug;
    void Start()
    {
        PanelImage = GetComponent<Image>();
    }
    
    // Update is called once per frame
    void Update()
    {
        filterDebug.text = PanelState.ToString();
    }
    public void ChangeCameraFilter()
    {
        PanelState = (PanelState + 1) % 4;
        ChangePanelColor();
        foreach(GameObject obj in ARP.spawnList)
        {
             if (PanelState == obj.GetComponent<Enemy>().part)
             {
                 obj.SetActive(true);
             }
             else
             {
                 obj.SetActive(false);
             }
        }
        foreach (GameObject obj in ARP.spawnItemList)
        {
            if (PanelState == 0)
            {
                obj.SetActive(true);
            }
            else if (PanelState != 0)
            {
                obj.SetActive(false);
            }
        }
    }
    public void ChangePanelColor()
    {
        if(PanelState == 0)
        {
            PanelImage.color = new Color(1,1,1,0);
        }
        if(PanelState == 1)
        {
            PanelImage.color = new Color(1, 0, 0, 0.3f);
        }
        if (PanelState == 2)
        {
            PanelImage.color = new Color(0, 1, 0, 0.3f);
        }
        if (PanelState == 3)
        {
            PanelImage.color = new Color(0, 0, 1, 0.3f);
        }
    }
}
