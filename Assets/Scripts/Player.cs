using UnityEngine;

public class Player : MonoBehaviour
{
    public GMScript gameManager;
    public int health = 100;
    public int maxHealth = 100;
    public string[] moveList = { "Fireball", "Tail Swipe" };
    public int damage = 20;

    void Start()
    {
        gameManager = FindAnyObjectByType<GMScript>();
    }

    public bool TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            health = 0;
            return true;
        }

        return false;
    }

    public void Attack(Player target)
    {
        bool isDead = target.TakeDamage(damage);
        if (isDead)
        {
            Debug.Log(target.name + " ha muerto.");
        }
        else
        {
            Debug.Log(target.name + " tiene " + target.health + " de salud.");
        }
    }

    public void PlayerDetected()
    {
        gameManager.AddPlayer(this);
    }
}
