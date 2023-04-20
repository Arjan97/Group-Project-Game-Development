using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTreeZoneController : MonoBehaviour
{
    public bool canAccessAbilityTrees = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canAccessAbilityTrees = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canAccessAbilityTrees = false;
        }
    }
}
