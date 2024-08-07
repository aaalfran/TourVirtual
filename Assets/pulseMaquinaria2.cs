using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pulseMaquinaria2 : MonoBehaviour
{

    public Button btnRt;
    public Button Caida;
    public Button btndespegue;

    public void Start()
    {

        StartCoroutine(PulseButtonEffect2(btnRt));
        StartCoroutine(PulseButtonEffect2(Caida));
        StartCoroutine(PulseButtonEffect2(btndespegue));
    }

    IEnumerator PulseButtonEffect2(Button button)
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
