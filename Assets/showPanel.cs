using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showPanel : MonoBehaviour
{
    public Button button;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        button.onClick.AddListener(ActivatePanelMethod);
    }

    void ActivatePanelMethod()
    {
        // Activa el panel
        panel.SetActive(true);
    }
}
