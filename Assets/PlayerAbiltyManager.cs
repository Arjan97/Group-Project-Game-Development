using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbiltyManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AbilityManager abilityManager;
    private Fireball fireball;
    private GroundSlam groundSlam;
    private Iceball iceball;
    private GroundPound groundPound;
    private ShockSlam shockSlam;

    void Start()
    {
        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && abilityManager.CanUseAbility("Fireball"))
        {
            fireball.FireFireball();
            abilityManager.StartAbilityCooldown(fireball.fireballCooldownTime);
        }

        if (Input.GetMouseButtonDown(1) && abilityManager.CanUseAbility("Iceball"))
        {
            iceball.FireIceball();
            abilityManager.StartAbilityCooldown(iceball.iceballCooldownTime);
        }
        if (Input.GetKeyDown(KeyCode.Space) && abilityManager.CanUseAbility("GroundSlam"))
        {
            abilityManager.StartAbilityCooldown(groundSlam.groundslamCooldownTime);
            groundSlam.GroundSlammer();
        }

        //groundSlam.radius = AbilityManager.instance.GetGroundSlamRadius();
        //groundSlam.damage = AbilityManager.instance.GetGroundSlamDamage();
        //groundSlam.groundslamCooldownTime = AbilityManager.instance.GetGroundSlamCooldown();

        if (Input.GetKeyDown(KeyCode.V) && abilityManager.CanUseAbility("GroundPound"))
        {
            Debug.Log("using ground pound");
            abilityManager.StartAbilityCooldown(groundPound.groundPoundCooldownTime);
            groundPound.GroundPounder();
        }

        //groundPound.radius = AbilityManager.instance.GetGroundPoundRadius();
        //groundPound.damage = AbilityManager.instance.GetGroundPoundDamage();
        //groundPound.groundPoundCooldownTime = AbilityManager.instance.GetGroundPoundCooldown();
        //groundPound.trembleDuration = AbilityManager.instance.GetGroundPoundTrembleDuration();

        if (Input.GetKeyDown(KeyCode.C) && abilityManager.CanUseAbility("ShockSlam"))
        {
            Debug.Log("using shock slam");
            abilityManager.StartAbilityCooldown(shockSlam.shockSlamCooldownTime);
            shockSlam.ShockSlammer();
        }

        //shockSlam.radius = AbilityManager.instance.GetShockSlamRadius();
        //shockSlam.damage = AbilityManager.instance.GetShockSlamDamage();
        //shockSlam.knockbackForce = AbilityManager.instance.GetShockSlamKnockbackForce();
        //shockSlam.stunDuration = AbilityManager.instance.GetShockSlamStunDuration();
        //shockSlam.shockSlamCooldownTime = AbilityManager.instance.GetShockSlamCooldown();
    }
}
