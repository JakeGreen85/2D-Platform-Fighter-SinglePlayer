using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charge_ultimate_script : Item
{
    public override void Activate(GameObject player)
    {
        player.GetComponent<PlayerController>().IncreaseUltCharge(10);
    }
}
