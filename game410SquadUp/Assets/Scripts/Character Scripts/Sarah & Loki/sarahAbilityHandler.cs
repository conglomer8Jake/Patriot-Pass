using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sarahAbilityHandler : playerAbilityHandler
{
    protected override void LaunchUlt1()
    {
        Instantiate(sandstorm, opposingPlayer.transform.position, Quaternion.identity, opposingPlayer.transform);
    }

    protected override void LaunchUlt2()
    {
        dM.setGravityManip();
    }

    protected override void LaunchUlt3()
    {
        lFH.lokiState = "aggressive";
    }
}
