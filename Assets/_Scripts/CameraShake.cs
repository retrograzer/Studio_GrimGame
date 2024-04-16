using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 originalPos;

    private void Awake()
    {
        originalPos = transform.localPosition;
    }

    public IEnumerator Shake (float duration, float magnitude)
    {
        //Debug.Log("CameraShake");
        float elapsed = 0.0f;

        originalPos = transform.localPosition;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
