using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Manager")]
    public GMScript gameManager;
<<<<<<< Updated upstream
    public int health = 100;
    public int maxHealth = 100;
    public string[] moveList = { "Fireball", "Tail Swipe" };
    public int damage = 20;
=======

    [Header("Pokemon Data")]
    public string pkmName;
    public int pkmMaxHP = 100;

    [Header("Moves and Attacks")]
    public string[] moveNames = new string[4];
    public int[] moveDamage = new int[4];

    [HideInInspector] public bool isTurn;
    [HideInInspector] public bool isDead;
    [HideInInspector] public int pkmCurrentHP;
>>>>>>> Stashed changes

    void Start()
    {
        gameManager = FindAnyObjectByType<GMScript>();
        pkmCurrentHP = pkmMaxHP;
        isTurn = false;
        isDead = false;
    }

<<<<<<< Updated upstream
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
=======
    public void TakeDamage(int _damage)
    {
        pkmCurrentHP -= _damage;
        Debug.Log(pkmCurrentHP.ToString());
        if (pkmCurrentHP <= 0)
        {
            isDead = true;
            PlayerLost();
>>>>>>> Stashed changes
        }
    }

    public void PlayerDetected()
    {
        if (!isDead)
        {
            gameManager.AddPlayer(this);
            Debug.Log(pkmName + " entered");
            RenderPlayer(true);
        }
    }

    public void PlayerLost()
    {
        gameManager.RemovePlayer(this);
        Debug.Log(pkmName + " exited");
        RenderPlayer(false);
    }

    public void RenderPlayer(bool _active)
    {
        GetComponent<Renderer>().enabled = _active;
    }
}
