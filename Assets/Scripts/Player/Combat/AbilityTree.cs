using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTree : MonoBehaviour
{
    private List<string> abilities;
    public string treeName { get; set; }
    public AbilityTree(string treeName)
    {
        this.treeName = treeName;
        abilities = new List<string>();
    }
    public void AddAbility(string abilityName)
    {
        abilities.Add(abilityName);
    }
    public bool IsAbilityInActiveTree(string abilityName)
    {
        return abilities.Contains(abilityName) && AbilityManager.instance.IsAbilityUnlocked(abilityName);
    }
}
