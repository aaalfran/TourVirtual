using UnityEngine;

public class PulseEffect : MonoBehaviour
{
    public float pulseSpeed = 1.5f; // Velocidad del latido
    public float minScale = 0.9f;   // Escala mínima durante el latido
    public float maxScale = 1.1f;   // Escala máxima durante el latido

    private RectTransform rectTransform;
    private Vector3 originalScale;
    private bool pulsing = true; // Mantener en true para que siempre esté latiendo

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }

    void Update()
    {
        if (pulsing)
        {
            // Efecto de latido
            float scale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(Time.time * pulseSpeed, 1));
            rectTransform.localScale = originalScale * scale;
        }
    }
}

