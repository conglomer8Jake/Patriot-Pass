using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starshipAbilityHandler : playerAbilityHandler
{
    protected override void LaunchUlt1()
    {
        pMH.speedUpPlayer();
    }

    protected override void LaunchUlt2()
    {
        dM.SHtwoBarUlt();
    }

    protected override void LaunchUlt3()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(starshipUltPrefab, new Vector3(pMH.transform.position.x + (offsetX), pMH.transform.position.y - i * 5.0f, 0.0f), Quaternion.identity);
        }
    }
}
