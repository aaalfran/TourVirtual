using UnityEngine;
using UnityEngine.UI;

public class TourManager : MonoBehaviour
{
    public GameObject menuBienvenida;
    public Button Empezar_Tour;
    public Button[] tourSteps;

    private int currentStep = 0;

    void Start()
    {
        // Asignar eventos a los botones
        Empezar_Tour.onClick.AddListener(StartTour);

        // Asignar el evento para los botones del tour
        foreach (Button button in tourSteps)
        {
            button.gameObject.SetActive(false); // Desactivar todos los botones al inicio
            button.onClick.AddListener(NextStep);
        }
    }

    void StartTour()
    {
        // Ocultar el panel de bienvenida
        menuBienvenida.SetActive(false);

        // Mostrar el primer botón del tour
        if (tourSteps.Length > 0)
        {
            tourSteps[0].gameObject.SetActive(true);
        }
    }

    void NextStep()
    {
        // Ocultar el botón actual
        if (currentStep < tourSteps.Length)
        {
            tourSteps[currentStep].gameObject.SetActive(false);
        }

        // Mostrar el siguiente botón si existe
        currentStep++;
        if (currentStep < tourSteps.Length)
        {
            tourSteps[currentStep].gameObject.SetActive(true);
        }
    }
}
