using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityInput : MonoBehaviour
{
    public Fireball fireball;
    public Iceball iceball;
    // Start is called before the first frame update
    void Start()
    {
        //fireball.GetComponent<Fireball>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && fireball.canShoot)
        {
            fireball.FireFireball();
        }
        if (Input.GetMouseButtonDown(1) && iceball.canShoot)
        {
            iceball.FireIceball();  
        }
    }
}
