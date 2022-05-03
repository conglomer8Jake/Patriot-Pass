using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class garymungerAbilityHandler : playerAbilityHandler
{
    protected override void LaunchUlt1()
    {
        dM.garyCrossbow();
    }

    protected override void LaunchUlt2()
    {
        dM.bellySlam();
        Debug.Log("Belly Slam");
    }

    protected override void LaunchUlt3()
    {
        int rand = Random.Range(0, 4);
        for (int i = 0; i < rand; i++)
        {
            officerOffsetY = Random.Range(-6.0f, 6.0f);
            Instantiate(officerPrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + officerOffsetY, 0.0f), Quaternion.identity);
        }
        Debug.Log("ult3");
    }
}
