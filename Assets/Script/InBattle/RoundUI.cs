using System.Collections;
using TMPro;
using UnityEngine;

public class RoundUI : MonoBehaviour
{
    [SerializeField] private TMP_Text currentRound;
    [SerializeField] private TMP_Text maxRound;

    [SerializeField] private float moveDistance = 30f;
    [SerializeField] private float duration = 0.15f;

    private RectTransform currentRect;
    private Vector2 defaultPos;

    private void Awake()
    {
        currentRect = currentRound.rectTransform;
        defaultPos = currentRect.anchoredPosition;
    }

    public void Init(int current, int max)
    {
        currentRound.text = current.ToString();
        maxRound.text = max.ToString();
    }

    public void UpdateRound(int round)
    {
        StopAllCoroutines();
        StartCoroutine(PlayAnimation(round));
    }

    private IEnumerator PlayAnimation(int round)
    {
        // Trượt xuống
        yield return Move(defaultPos.y, -moveDistance);

        // Teleport lên trên + đổi số
        currentRect.anchoredPosition = new Vector2(defaultPos.x, moveDistance);
        currentRound.text = round.ToString();

        // Trượt về giữa
        yield return Move(moveDistance, defaultPos.y);
    }

    private IEnumerator Move(float fromY, float toY)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float y = Mathf.Lerp(fromY, toY, time / duration);

            currentRect.anchoredPosition =
                new Vector2(defaultPos.x, y);

            yield return null;
        }

        currentRect.anchoredPosition =
            new Vector2(defaultPos.x, toY);
    }
}