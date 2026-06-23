using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject HUDBar;

    [Header("Color")]
    [SerializeField] private Color friendlyColor = Color.green;
    [SerializeField] private Color enemyColor = Color.red;

    [HideInInspector]
    public CharacterManager owner;

    public TypeTeam typeTeam = TypeTeam.None;

    public float maxHealth;
    public float maxMana;

    public float currentHealth;
    public float currentMana;

    public bool IsDead { get; private set; }

    private Animator anim;
    private TypeTeam lastTeam;

    private void Start()
    {
        if (owner == null)
            owner = GetComponentInParent<CharacterManager>();

        if (owner != null)
        {
            if (owner.Stats.MHealth <= 0)
                owner.Stats.InitializeStats();

            maxHealth = owner.Stats.MHealth;
            maxMana = owner.Stats.MMana;

            currentHealth = maxHealth;
            currentMana = maxMana;
        }

        anim = GetComponent<Animator>();

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
}