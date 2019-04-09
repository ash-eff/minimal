using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Color A;
    public Color B;
    public float speed = 2;

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }

    public IEnumerator ColorLerp()
    {
        float currentLerpTime = 0;
        Camera.main.backgroundColor = A;

        while (Camera.main.backgroundColor!= B)
        {
            currentLerpTime += Time.deltaTime;
            float perc = currentLerpTime * speed;
            Camera.main.backgroundColor = Color.Lerp(A, B, perc);
            yield return null;
        }
    }
}
