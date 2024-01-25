using System.Collections;
using UnityEngine;

public class PulsatingObject : MonoBehaviour
{
    public float pulseInterval = 0.5f;
    public float pulseScaleFactor = 0.6f;
    public float pulseDuration = 0.1f; // Adjust as needed for the duration of the pulsating effect

    private Vector3 originalScale;
    private Coroutine pulsateCoroutine;

    void Start()
    {
        originalScale = transform.localScale;
        StartPulsating();
    }

    void StartPulsating()
    {
        if (pulsateCoroutine == null)
        {
            pulsateCoroutine = StartCoroutine(Pulsate());
        }
    }

    IEnumerator Pulsate()
    {
        while (true)
        {
            yield return Pulse();
            yield return new WaitForSeconds(pulseInterval);
        }
    }

    IEnumerator Pulse()
    {
        float elapsedTime = 0f;
        Vector3 targetScale = originalScale * pulseScaleFactor;

        while (elapsedTime < pulseDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / pulseDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;

        yield return new WaitForSeconds(0.1f); // Adjust the duration as needed for a pause

        // Reset to original scale
        elapsedTime = 0f;
        while (elapsedTime < pulseDuration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / pulseDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }
}
