using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shutDownPanel : MonoBehaviour
{

    public Button Button;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        Button.onClick.AddListener(DeActivatePanelMethod);
    }

    void DeActivatePanelMethod()
    {
        // Activa el panel
        panel.SetActive(false);
    }
}
