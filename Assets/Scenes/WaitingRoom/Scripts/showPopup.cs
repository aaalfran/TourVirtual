using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class showPopup : MonoBehaviour
{
    public InputActionReference popUpReference = null;
    public string mainMenuSceneName = "WaitingRoom";

    private void Awake()
    {
        
    }
    private void Update() => SceneManager.LoadScene(mainMenuSceneName);

    private void showpopUp()
    {
        
    }
}
