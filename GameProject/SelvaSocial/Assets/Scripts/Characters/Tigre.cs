using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tigre : Character
{

    void OnTriggerEnter(Collider other)
    {

    }

    public override void NormalAbility(Transform target)
    {
        Debug.Log("Atack");
    }

    public override void ChargeAbility(Transform target)
    {
        Debug.Log("CAtack");
    }

    public override void SupportAbility()
    {
        Debug.Log("Suport");
    }
}
