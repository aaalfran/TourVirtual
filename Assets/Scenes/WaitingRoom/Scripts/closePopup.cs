using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class closeMenu : MonoBehaviour
{
    public void shutDown(GameObject menupanel)
    {
        menupanel.SetActive(false);
    }
}

