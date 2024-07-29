using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Update()
    {
        // Asegurarse de que el objeto de texto siempre mire hacia la cámara
        transform.LookAt(Camera.main.transform);
        
        // Ajustar la rotación para que el texto esté recto y de frente
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
