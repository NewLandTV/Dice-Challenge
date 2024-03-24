using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private float shakeTime;
    private float shakeIntensity;

    public void Shake(float shakeTime = 1f, float shakeIntensity = 0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StopCoroutine(OnShakeByRotation());
        StartCoroutine(OnShakeByRotation());
    }

    private IEnumerator OnShakeByRotation()
    {
        Vector3 start = transform.eulerAngles;

        while (shakeTime > 0f)
        {
            transform.rotation = Quaternion.Euler(start + new Vector3(0f, 0f, Random.Range(-1f, 1f)) * shakeIntensity);

            shakeTime -= Time.deltaTime;

            yield return null;
        }

        transform.rotation = Quaternion.Euler(start);
    }
}
