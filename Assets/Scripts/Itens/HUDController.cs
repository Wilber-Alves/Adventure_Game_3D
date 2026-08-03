using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
   [Header("Monsters Settings")]
    public TextMeshProUGUI monsterText;
    public SOInt monsterKillSO;
    public int totalMonstersInLevel = 12;

    [Header("Health Settings")]
    public Slider healthSlider;
    public SOFloat_Health SOFloat_Health;
    public float maxHealth = 30f;

    [Header("Weapon Settings")]
    public Image weaponIcon;

    [Header("Coins Settings")]
    public TextMeshProUGUI coinText;
    public SOFloat soFloat;

    void Start()
    {

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }

        if (weaponIcon != null) weaponIcon.enabled = false;
    }

    void Update()
    {

        if (SOFloat_Health != null && healthSlider != null)
        {
            healthSlider.value = SOFloat_Health.value;
        }


        if (InventoryBase.hasWeapon && weaponIcon != null)
        {
            weaponIcon.enabled = true;
        }


        if (soFloat != null && coinText != null)
        {
            coinText.text = soFloat.valueFloat.ToString("F0");
        }

        if (monsterKillSO != null && monsterText != null)
        {
            monsterText.text = $"Monstros: {monsterKillSO.valueInt} / {totalMonstersInLevel}";
        }

    }

}

