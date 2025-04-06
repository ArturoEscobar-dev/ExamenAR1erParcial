using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Manager")]
    public GMScript gameManager;

    [Header("Pokemon Data")]
    public string pkmName;
    public int pkmMaxHP = 100;
    public int damage = 20;

    [Header("Moves and Attacks")]
    public string[] moveNames = new string[4];
    public int[] moveDamage = new int[4];

    [HideInInspector] public bool isTurn;
    [HideInInspector] public bool isDead;
    [HideInInspector] public int pkmCurrentHP;

    void Start()
    {
        gameManager = FindAnyObjectByType<GMScript>();
        pkmCurrentHP = pkmMaxHP;
        isTurn = false;
        isDead = false;
    }

    public bool TakeDamage(int damageAmount)
    {
        pkmCurrentHP -= damageAmount;

        if (pkmCurrentHP <= 0)
        {
            pkmCurrentHP = 0;
            return true;
        }

        return false;
    }

    public void Attack(Player target)
    {
        bool isDead = target.TakeDamage(damage);
        if (isDead)
        {
            Debug.Log(target.pkmName + " ha muerto.");
            target.isDead = true;
            target.PlayerLost();
        }
        else
        {
            Debug.Log(target.pkmName + " tiene " + target.pkmCurrentHP + " de salud."); 
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
