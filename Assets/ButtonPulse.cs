using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonPulse : MonoBehaviour
{
    public Button button;
    public Button move;
    public Button Caida;
    public Button botonAtrapamiento;

    void Start()
    {
        StartCoroutine(PulseButtonEffect(button));
        StartCoroutine(PulseButtonEffect(move));
        StartCoroutine(PulseButtonEffect(Caida));
        StartCoroutine(PulseButtonEffect(botonAtrapamiento));
    }

    IEnumerator PulseButtonEffect(Button button)
    {
        while (true)
        {
            // Efecto de agrandar
            for (float i = 1f; i <= 1.1f; i += 0.01f)
            {
                button.transform.localScale = new Vector3(i, i, 1f);
                yield return new WaitForSeconds(0.05f);
            }

            // Efecto de encoger
            for (float i = 1.1f; i >= 1f; i -= 0.01f)
            {
                button.transform.localScale = new Vector3(i, i, 1f);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
