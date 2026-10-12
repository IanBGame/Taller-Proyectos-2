using UnityEngine;

[System.Serializable]

public class PlayerStats
{
    public int playerID;
    public int roundWins = 0;

    public float playerSpeed = 5f;
    public float Health = 7f;
    public float shootTime = 1f;
    public float shootRange = 3f;
    public float projectileSize = 1f;

    public int skinID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
