using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health_script : Item
{
    public override void Activate(GameObject player)
    {
        player.GetComponent<PlayerController>().TakeDamage(-20);
    }
}
