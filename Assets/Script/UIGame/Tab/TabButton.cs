using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TabButton : MonoBehaviour
{
    [SerializeField] private RectTransform target;
    [SerializeField] private TMP_Text text;

    [Header("Selected")]
    [SerializeField] private float moveDistance = 30f;
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color normalColor = Color.white;

    private Vector2 defaultPos;
    private Coroutine anim;

    private void Awake()
    {
        defaultPos = target.anchoredPosition;
    }

    public void Select()
    {
        MoveTo(defaultPos + Vector2.right * moveDistance);


        if (text != null)
            text.color = selectedColor;
    }

    public void Deselect()
    {
        MoveTo(defaultPos);


        if (text != null)
            text.color = normalColor;
    }

    void MoveTo(Vector2 targetPos)
    {
        if (anim != null)
            StopCoroutine(anim);

        anim = StartCoroutine(Animate(targetPos));
    }

    IEnumerator Animate(Vector2 targetPos)
    {
        Vector2 start = target.anchoredPosition;
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;

            target.anchoredPosition =
                Vector2.Lerp(start, targetPos, t / duration);

            yield return null;
        }

        target.anchoredPosition = targetPos;
    }
}