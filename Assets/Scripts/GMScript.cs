using System.Collections.Generic;
using UnityEngine;

public class GMScript : MonoBehaviour
{
    public List<Player> players = new List<Player>();

    public void AddPlayer(Player _player)
    {
        players.Add(_player);
    }
}
