using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraFilter : MonoBehaviour
{
    // Start is called before the first frame update
    public Image PanelImage;
    private int PanelState = 0;
    void Start()
    {
        PanelImage = GetComponent<Image>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeCameraFilter()
    {
        PanelState = (PanelState + 1) % 4;
        ChangePanelColor();
    }
    public void ChangePanelColor()
    {
        if(PanelState == 0)
        {
            PanelImage.color = new Color(1,1,1,0);
        }
        if(PanelState == 1)
        {
            PanelImage.color = new Color(1, 0, 0, 0.5f);
        }
        if (PanelState == 2)
        {
            PanelImage.color = new Color(0, 1, 0, 0.5f);
        }
        if (PanelState == 3)
        {
            PanelImage.color = new Color(0, 0, 1, 0.5f);
        }
    }
}
