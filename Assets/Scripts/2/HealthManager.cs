using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    const int maxHealth = 3;
    int playerHealth = 3;
    public Image[] hearts;

    private void Awake()
    {
        instance = this;
    }
    public void ChangeHealth(int val)
    {
        playerHealth = Mathf.Clamp(playerHealth + val, 0, maxHealth);
        for(int i = 0; i < maxHealth; i++)
        {
            if (i <= playerHealth)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }
}
