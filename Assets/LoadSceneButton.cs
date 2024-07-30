using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    // Nombre de la escena a cargar
    public string sceneName = "Maquinaria2";

   public  void Start()
    {
        // Obtener el componente Button y añadir el listener al evento OnClick
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("No Button component found on this GameObject.");
        }
    }

    // Método que se llama cuando se hace clic en el botón
    void OnButtonClick()
    {
        // Cargar la escena especificada
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("No scene name specified.");
        }
    }
}
