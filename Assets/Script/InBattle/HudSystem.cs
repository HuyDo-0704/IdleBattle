using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public enum TypeTeam
{
    None,
    Player,
    Enemy
}

public class HudSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image healthFill;
    [SerializeField] private Image manaFill;
    public GameObject HUDBar;

    [Header("Color")]
    [SerializeField] private Color friendlyColor = Color.green;
    [SerializeField] private Color enemyColor = Color.red;

    [HideInInspector]
    public CharacterManager owner;

    public TypeTeam typeTeam = TypeTeam.None;
    // health
    public float maxHealth;
    public float currentHealth;
    // mana
    public float currentMana;
    private float maxMana = 100f;
    public bool IsDead { get; private set; }

    private Animator anim;
    private TypeTeam lastTeam;
    [Header("Damage Feedback")]
    [SerializeField] private float shakeDuration = 0.15f;
    [SerializeField] private float shakeAmount = 0.05f;

    private Vector3 hudBarOriginalPos;
    private Coroutine shakeCoroutine;

    private void Start()
    {
        if (typeTeam == TypeTeam.None)
            return;
        if (owner == null)
            owner = GetComponentInParent<CharacterManager>();
        if (owner != null)
        {
            if (owner.CChartacter.currentStats.FinalStats.hp <= 0 )
                owner.CChartacter.currentStats.RecalculateStats(owner.CChartacter.CurrentLevel);

            maxHealth = owner.CChartacter.currentStats.FinalStats.hp;

            currentHealth = maxHealth;
            currentMana = 0 + owner.CChartacter.currentStats.FinalStats.manaBonus;
        }

        anim = GetComponent<Animator>();

        if (HUDBar != null)
        hudBarOriginalPos = HUDBar.transform.localPosition;
        UpdateDirection();
        UpdateUI();
    }

    private void Update()
    {
        if (lastTeam != typeTeam)
        {
            lastTeam = typeTeam;
            UpdateDirection();
        }
    }

    private void UpdateUI()
    {
        if (healthFill != null && maxHealth > 0)
            healthFill.fillAmount = currentHealth / maxHealth;

        if (manaFill != null && maxMana > 0)
            manaFill.fillAmount = currentMana / maxMana;

        UpdateColor();
    }
    public void SetTeam(TypeTeam team)
    {
        typeTeam = team;
        lastTeam = team;

        UpdateDirection();
        UpdateColor();
    }
    private void UpdateColor()
    {
        if (healthFill == null) return;

        healthFill.color =
            typeTeam == TypeTeam.Player
            ? friendlyColor
            : enemyColor;
    }

    private void UpdateDirection()
    {
        Vector3 scale = transform.localScale;

        scale.x = typeTeam == TypeTeam.Player
            ? -Mathf.Abs(scale.x)
            : Mathf.Abs(scale.x);

        transform.localScale = scale;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (anim != null)
            anim.SetTrigger("Damaged");

        if (HUDBar != null)
        {
            if (shakeCoroutine != null)
                StopCoroutine(shakeCoroutine);

            shakeCoroutine = StartCoroutine(ShakeHUDBar());
        }

        if (currentHealth <= 0)
            Die();

        UpdateUI();
    }

    public void Heal(float amount)
    {
        if (IsDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        UpdateUI();
    }

    public void UseMana(float amount)
    {
        currentMana -= amount;
        currentMana = Mathf.Max(currentMana, 0);
        UpdateUI();
    }

    public void RestoreMana(float amount)
    {
        currentMana += amount;
        currentMana = Mathf.Min(currentMana, maxMana);
        UpdateUI();
    }

    private void Die()
    {
        if (IsDead) return;

        IsDead = true;

        if (anim != null)
            anim.SetTrigger("Death");

        if (HUDBar != null)
            HUDBar.SetActive(false);
    }
    // shake hudBar
    private IEnumerator ShakeHUDBar()
    {
        float timer = 0f;

        while (timer < shakeDuration)
        {
            timer += Time.deltaTime;

            Vector3 offset = new Vector3(
                Random.Range(-shakeAmount, shakeAmount),
                Random.Range(-shakeAmount, shakeAmount),
                0f);

            HUDBar.transform.localPosition =
                hudBarOriginalPos + offset;

            yield return null;
        }

        HUDBar.transform.localPosition = hudBarOriginalPos;
    }
}