using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieterichAbilityHandler : playerAbilityHandler
{
    protected override void LaunchUlt1()
    {
        opposingPlayer.GetComponent<playerAbilityHandler>().inhibitUltGain();
    }

    protected override void LaunchUlt2()
    {
        opposingPlayer.GetComponent<playerAbilityHandler>().ultBar--;   
    }

    protected override void LaunchUlt3()
    {
        dM.theoryOfPower();
    }
}
