using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    private int FireBallTime = 0;
    private int FireBallCounter = 20;

    public Fireball fireball;

    [SerializeField] private Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FireBallTime++;
        if(FireBallCounter <= FireBallTime)
        {
            //fireball.FireFireball();
            FireBallTime = 0;
        }

        
    }
    private void FixedUpdate()
    {
        transform.LookAt(player);
    }
}
