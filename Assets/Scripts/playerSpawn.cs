using UnityEngine;

public class playerSpawn : MonoBehaviour
{
    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = transform.position;
    }
}