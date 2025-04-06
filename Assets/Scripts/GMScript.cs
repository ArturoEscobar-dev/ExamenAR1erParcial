using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYER1TURN, PLAYER2TURN, WON, LOST }

public class GMScript : MonoBehaviour
{
    public List<Player> players = new List<Player>();
    public BattleState state;

    public void AddPlayer(Player _player)
    {
        players.Add(_player);
    }

    void Start()
    {
        state = BattleState.START;
        StartBattle();
    }

    void StartBattle()
    {
        Player player1 = players[0];
        Player player2 = players[1];

        state = BattleState.PLAYER1TURN;
        Player1Turn(player1, player2);
    }

    void Player1Turn(Player player1, Player player2)
    {
        Debug.Log("Jugador 1 ataca");
        bool isDead = player2.TakeDamage(player1.damage);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYER2TURN;
            Player2Turn(player1, player2);
        }
    }

    void Player2Turn(Player player1, Player player2)
    {
        Debug.Log("Jugador 2 ataca");
        bool isDead = player1.TakeDamage(player2.damage);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYER1TURN;
            Player1Turn(player1, player2);
        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
            Debug.Log("¡Jugador 1 gana!");
        else if (state == BattleState.LOST)
            Debug.Log("¡Jugador 2 gana!");
    }
}
