using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nr : MonoBehaviour
{
    public PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
