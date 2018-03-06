using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "LittleFire/Weapon", order = 1)]
public class Weapon : ScriptableObject {
    public int glanceChance = 15;
    public int criticalChance = 20;

    public int glanceDamage = 5;
    public int normalDamage = 10;
    public int criticalDamage = 20;

    public int minRange = 1;
    public int maxRange = 1;

    public void DealDamage(UnitDetails target)
    {
        int hit = Random.Range(0, 100);

        if (hit <= glanceChance)
        {
            target.Health -= glanceDamage;
            Debug.Log("A glancing blow...");
        }
        else if (hit >= 100 - criticalChance)
        {
            target.Health -= criticalDamage;
            Debug.Log("A critical hit!");
        }
        else
            target.Health -= normalDamage;
    }
}
