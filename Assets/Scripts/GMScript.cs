using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using static UnityEditor.Experimental.GraphView.GraphView;

public enum BattleState { START, PLAYER1TURN, PLAYER2TURN, WON, LOST }

public class GMScript : MonoBehaviour
{
    public List<Player> players = new List<Player>();

    [Header("Manager info")]
    public BattleState state;
    public GameObject buttons;

    [Header("Texts")]
    public TextMeshProUGUI[] buttonText = new TextMeshProUGUI[4];
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

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
            state = BattleState.START;
        }
    }

    void Start()
    {
        state = BattleState.START;
        buttons.SetActive(false);
        dialogueBox.SetActive(false);
        //StartBattle();
    }

    void StartBattle()
    {
        Player player1 = players[0];
        Player player2 = players[1];

        buttons.SetActive(true);
        dialogueBox.SetActive(true);

        state = BattleState.PLAYER1TURN;
        PlayerTurn(player1, player2);
    }

    void PlayerTurn(Player _activePlayer, Player _inactivePlayer)
    {
        SetTurn(_activePlayer, _inactivePlayer);
        SetUIBattleNames(_activePlayer);
    }

    void EndBattle()
    {
        if (state == BattleState.PLAYER1TURN)
        {
            Debug.Log("Player 1 wins");
        }
        else if (state == BattleState.PLAYER2TURN)
        {
            Debug.Log("Player 2 wins");
        }

        state = BattleState.WON;
    }

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

    void HandleDamage(Player _activePlayer, Player _inactivePlayer, int _damageToDeal)
    {
        Debug.Log(_activePlayer.pkmName + " ataca");
        bool isDead = _inactivePlayer.TakeDamage(_damageToDeal);

        if (isDead)
        {
            //state = BattleState.WON;
            EndBattle();
        }
        else
        {
            if (state == BattleState.PLAYER1TURN)
            {
                state = BattleState.PLAYER2TURN;
            }
            else if (state == BattleState.PLAYER2TURN)
            {
                state = BattleState.PLAYER1TURN;
            }
            
            PlayerTurn(_inactivePlayer, _activePlayer);
        }
    }

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

    //Ligar estas funciones a los botones del UI
    public void Attack1()
    {
        Player activePlayer = GetTurnPlayer();
        Player inactivePlayer = GetNonTurnPlayer();
        HandleDamage(activePlayer, inactivePlayer, activePlayer.moveDamage[0]);
    }

    public void Attack2()
    {
        Player activePlayer = GetTurnPlayer();
        Player inactivePlayer = GetNonTurnPlayer();
        HandleDamage(activePlayer, inactivePlayer, activePlayer.moveDamage[1]);
    }

    public void Attack3() 
    {
        Player activePlayer = GetTurnPlayer();
        Player inactivePlayer = GetNonTurnPlayer();
        HandleDamage(activePlayer, inactivePlayer, activePlayer.moveDamage[2]);
    }

    public void Attack4()
    {
        Player activePlayer = GetTurnPlayer();
        Player inactivePlayer = GetNonTurnPlayer();
        HandleDamage(activePlayer, inactivePlayer, activePlayer.moveDamage[3]);
    }

    //void Player1Turn(Player player1, Player player2)
    //{
    //    SetUIBattleNames(player1);
    //    SetTurn(player1, player2);

    //    Debug.Log("Jugador 1 ataca");
    //    bool isDead = player2.TakeDamage(player1.damage);

    //    if (isDead)
    //    {
    //        state = BattleState.WON;
    //        EndBattle();
    //    }
    //    else
    //    {
    //        state = BattleState.PLAYER2TURN;
    //        Player2Turn(player1, player2);
    //    }
    //}

    //void Player2Turn(Player player1, Player player2)
    //{
    //    Debug.Log("Jugador 2 ataca");
    //    bool isDead = player1.TakeDamage(player2.damage);

    //    if (isDead)
    //    {
    //        state = BattleState.LOST;
    //        EndBattle();
    //    }
    //    else
    //    {
    //        state = BattleState.PLAYER1TURN;
    //        Player1Turn(player1, player2);
    //    }
    //}
}
