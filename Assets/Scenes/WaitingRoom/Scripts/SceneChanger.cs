using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(Button button)
    {
        string sceneName = button.name;
        SceneManager.LoadScene(sceneName);
    }
}

