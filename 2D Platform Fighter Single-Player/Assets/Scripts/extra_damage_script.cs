using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extra_damage_script : Item
{
    public override void Activate(GameObject player)
    {
        player.GetComponent<PlayerController>().IncreaseDamage(10);
    }
}
