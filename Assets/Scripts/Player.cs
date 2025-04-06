using UnityEngine;

public class Player : MonoBehaviour
{
    public GMScript gameManager;
    public int health = 100;
    public string[] moveList = {"Fireball", "Tail Swipe"};

    void Start()
    {
        gameManager = FindAnyObjectByType<GMScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDetected()
    {
        gameManager.AddPlayer(this);
    }
}
