using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    public static DamagePopupManager Instance;

    [SerializeField]
    private PopupDamageEffect popupPrefab;
    [SerializeField] private float popupHeightOffset = 1.5f;
    private void Awake()
    {
        Instance = this;
    }

    public void ShowDamage(
    Transform target,
    int damage)
    {
        Vector3 spawnPos =
            target.position + Vector3.up * popupHeightOffset;

        PopupDamageEffect popup =
            Instantiate(
                popupPrefab,
                spawnPos,
                Quaternion.identity);

        popup.SetText(damage.ToString());
    }
}