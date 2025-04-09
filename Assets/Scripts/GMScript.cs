using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Collections;

public enum BattleState { START, PLAYER1TURN, PLAYER2TURN, WON, LOST }

public class GMScript : MonoBehaviour
{
    public List<Player> players = new List<Player>();

    [Header("Manager info")]
    public GameObject buttons;
    public GameObject healthTextsObject;
    public GameObject dialogueBox;
    public AudioSource music;
    public AudioSource winSFX;

    [Header("Texts")]
    public TextMeshProUGUI[] buttonText = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] healthTexts = new TextMeshProUGUI[2];
    public TextMeshProUGUI dialogueText;

    //--------------Vuforia Add and remove Player--------------
    public void AddPlayer(Player _player)
    {
        if (players.Count < 2)
        {
            players.Add(_player);
            if (players.Count == 2)
            {
                StartBattle();
            }
        }
    }

    public void RemovePlayer(Player _player)
    {
        players.Remove(_player);
        if (players.Count < 2)
        {
            buttons.SetActive(false);
            dialogueBox.SetActive(false);
            healthTextsObject.SetActive(false);
            music.Stop();
        }
    }

    void Start()
    {
        buttons.SetActive(false);
        dialogueBox.SetActive(false);
        healthTextsObject.SetActive(false);
    }

    //--------------Battle Sequences--------------
    void StartBattle()
    {
        Player player1 = players[0];
        Player player2 = players[1];

        SetUIHealth(player1);
        SetUIHealth(player2);

        buttons.SetActive(true);
        dialogueBox.SetActive(true);
        healthTextsObject.SetActive(true);
        music.Play();

        PlayerTurn(player1, player2);
    }

    void PlayerTurn(Player _activePlayer, Player _inactivePlayer)
    {
        SetTurn(_activePlayer, _inactivePlayer);
        SetUIBattleNames(_activePlayer);
    }

    IEnumerator HandleDamage(Player _activePlayer, Player _inactivePlayer, int _damageToDeal)
    {

        dialogueText.text = _activePlayer.pkmName + " atacks";

        buttons.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        bool isDead = _inactivePlayer.TakeDamage(_damageToDeal);

        dialogueText.text = _inactivePlayer.pkmName + " takes " + _damageToDeal.ToString() + " damage";

        SetUIHealth(_inactivePlayer);

        yield return new WaitForSeconds(1.5f);

        if (isDead)
        {
            StartCoroutine(EndBattle(_activePlayer, _inactivePlayer));
        }
        else
        {
            buttons.SetActive(true);

            PlayerTurn(_inactivePlayer, _activePlayer);
        }
    }

    IEnumerator EndBattle(Player _playerWhoWon, Player _playerWhoLost)
    {
        dialogueText.text = _playerWhoWon.pkmName + " wins!";

        music.Stop();
        winSFX.Play();

        yield return new WaitForSeconds(2.5f);

        _playerWhoLost.PlayerDied();
    }

    //--------------Value and Text Setters--------------
    void SetUIBattleNames(Player _activePlayer)
    {
        for (int i = 0; i < _activePlayer.moveNames.Length; i++)
        {
            buttonText[i].text = _activePlayer.moveNames[i];
        }

        dialogueText.text = _activePlayer.pkmName + "'s turn";
    }

    void SetTurn(Player _activePlayer, Player _inactivePlayer)
    {
        _activePlayer.isTurn = true;
        _inactivePlayer.isTurn = false;
    }

    void SetUIHealth(Player _player)
    {
        healthTexts[players.IndexOf(_player)].text = _player.UpdateHealthText();
    }

    //--------------Player Turn Getters--------------

    Player GetTurnPlayer()
    {
        foreach (Player player in players)
        {
            if (player.isTurn)
            {
                return player;
            }
        }

        return null;
    }

    Player GetNonTurnPlayer()
    {
        foreach (Player player in players)
        {
            if (!player.isTurn)
            {
                return player;
            }
        }

        return null;
    }

    //--------------Functions for UI Buttons--------------
    public void Attack1()
    {
        Player activePlayer = GetTurnPlayer();
        Player inactivePlayer = GetNonTurnPlayer();
        StartCoroutine(HandleDamage(activePlayer, inactivePlayer, activePlayer.moveDamage[0]));
    }

    public void Attack2()
    {
        Player activePlayer = GetTurnPlayer();
        Player inactivePlayer = GetNonTurnPlayer();
        StartCoroutine(HandleDamage(activePlayer, inactivePlayer, activePlayer.moveDamage[1]));
    }

    public void Attack3() 
    {
        Player activePlayer = GetTurnPlayer();
        Player inactivePlayer = GetNonTurnPlayer();
        StartCoroutine(HandleDamage(activePlayer, inactivePlayer, activePlayer.moveDamage[2]));
    }

    public void Attack4()
    {
        Player activePlayer = GetTurnPlayer();
        Player inactivePlayer = GetNonTurnPlayer();
        StartCoroutine(HandleDamage(activePlayer, inactivePlayer, activePlayer.moveDamage[3]));
    }
}
