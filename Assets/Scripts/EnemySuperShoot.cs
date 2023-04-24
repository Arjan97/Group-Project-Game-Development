using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuperShoot : MonoBehaviour
{
    [SerializeField] Transform player;
    private Fireball fireball;
    private Iceball iceball;

    private float CooldownTime = 10f;
    private int ShootCounter = 0;

    int randomNumber;
    // Start is called before the first frame update
    private void Start()
    {
        fireball = GetComponent<Fireball>();
        iceball = GetComponent<Iceball>();
    }

    // Update is called once per frame
    private void Update()
    {
        randomNumber = UnityEngine.Random.Range(0, 2);
        ShootCounter++;
        transform.LookAt(player);

        if (ShootCounter >= CooldownTime)
        {
            Random();
            ShootCounter = 0;
        }
        print(randomNumber);
    }

    private void Random()
    {
        if(randomNumber == 1)
        {
            fireball.FireFireball();
        }

        else 
        { 
            iceball.FireIceball(); 
        }
    }
    
   
}
