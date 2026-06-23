using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    public static DamagePopupManager Instance;

    [SerializeField]
    private PopupDamageEffect popupPrefab;

    [SerializeField]
    private Canvas canvas;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowDamage(
        Transform target,
        int damage)
    {
        PopupDamageEffect popup =
            Instantiate(
                popupPrefab,
                canvas.transform);

        Vector2 screenPos =
            Camera.main.WorldToScreenPoint(
                target.position);

        popup.GetComponent<RectTransform>()
            .position = screenPos;

        popup.SetText(damage.ToString());
    }
}