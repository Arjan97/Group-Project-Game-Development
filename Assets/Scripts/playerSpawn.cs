using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = transform.position;
    }
}