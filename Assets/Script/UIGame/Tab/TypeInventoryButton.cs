using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeInventory : TabButton
{
    [SerializeField] private RectTransform target;
    [SerializeField] private TMP_Text text;

    [Header("Selected")]
    [SerializeField] private float moveDistance = 30f;
    [SerializeField] private float duration = 0.2f;
    private Vector2 defaultPos;
    private Coroutine anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        defaultPos = target.anchoredPosition;
    }

    public override  void Select()
    {
        base.Select();
        MoveTo(defaultPos + Vector2.right * moveDistance);


        if (text != null)
            text.color = Color.yellow;
    }

    public override void Deselect()
    {
        base.Deselect();
        MoveTo(defaultPos);


        if (text != null)
            text.color = Color.white;
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
