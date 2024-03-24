using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer diceSpriteRenderer;
    [SerializeField]
    private Sprite[] diceSprites;

    // Effects
    [SerializeField]
    private ShakeCamera shakeCamera;
    [SerializeField]
    private GameObject endEffect;

    [SerializeField]
    private float diceRollingWaitForSecondValue;

    private bool isRolling;

    private Vector3 originScale;

    private WaitForSeconds waitTime1_5f;
    private WaitForSeconds diceRollingWaitForSecond;
    private WaitForSeconds diceRollingWaitForSecondHalf;

    private void Awake()
    {
        originScale = transform.localScale;

        waitTime1_5f = new WaitForSeconds(1.5f);
        diceRollingWaitForSecond = new WaitForSeconds(diceRollingWaitForSecondValue);
        diceRollingWaitForSecondHalf = new WaitForSeconds(diceRollingWaitForSecondValue * 0.5f);
    }

    public void OnRollingButtonClick()
    {
        if (!isRolling)
        {
            StartCoroutine(RollingCoroutine());
        }
    }

    private IEnumerator RollingCoroutine()
    {
        isRolling = true;

        int index = 0;
        int loopCount = Random.Range(30, 50);

        shakeCamera.Shake(loopCount * diceRollingWaitForSecondValue, 1.5f);

        for (byte i = 0; i < loopCount; i++)
        {
            int prevIndex = index;

            while (prevIndex == index)
            {
                index = Random.Range(0, diceSprites.Length - 1);
            }

            diceSpriteRenderer.sprite = diceSprites[index];

            transform.localScale += Vector3.one * loopCount * diceRollingWaitForSecondValue * Time.deltaTime;

            yield return diceRollingWaitForSecond;
        }

        Debug.Log($"나온 숫자 : {index + 1}");

        endEffect.SetActive(true);

        Vector3 startScale = transform.localScale;

        for (byte i = 0; i <= 10; i++)
        {
            transform.localScale = Vector3.Lerp(startScale, originScale, i);

            yield return diceRollingWaitForSecondHalf;
        }

        yield return waitTime1_5f;

        endEffect.SetActive(false);

        isRolling = false;
    }
}
