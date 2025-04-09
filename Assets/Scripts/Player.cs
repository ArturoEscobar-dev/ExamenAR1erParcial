using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Manager")]
    public GMScript gameManager;

    [Header("Pokemon Data")]
    public string pkmName;
    public int pkmMaxHP = 100;

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

    //--------------Returns for Game Manager--------------
    public string UpdateHealthText()
    {
        return pkmName+ ": " + pkmCurrentHP + " / " + pkmMaxHP;
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

    //--------------Kill Player function--------------
    public void PlayerDied()
    {
        isDead = true;
        PlayerLost();
    }

    //--------------Vuforia Player found/lost--------------
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

    //--------------Appear/Dissapear player--------------
    public void RenderPlayer(bool _active)
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = _active;
        }
    }
}