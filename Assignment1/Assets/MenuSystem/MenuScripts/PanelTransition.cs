using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTransition : MonoBehaviour
{
    public GameObject NextPanel;
    // Start is called before the first frame update
   public void Next()
    {
        NextPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
