using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tartaruga : Character
{

    void Update()
    {
        if (!acting)
        {
            HurtBoxs[0].gameObject.SetActive(false);
            HurtBoxs[1].gameObject.SetActive(false);
        }
    }

    public override void NormalAbility(Transform target)
    {
        if (acting)
            return;

        acting = true;

        transform.position = new Vector3(target.position.x - 0.5f, target.position.y, target.position.z);
        controller.SetBool("Atack", true);
        HurtBoxs[0].gameObject.SetActive(true);
        currentAtack = 20;

        StartCoroutine("AbilityTime", "Atack");
    }

    public override void ChargeAbility(Transform target)
    {
        if (acting)
            return;

        acting = true;

        controller.SetBool("Charge", true);
        HurtBoxs[1].SetActive(true);
        currentAtack = 30;

        StartCoroutine("AbilityTime", "Charge");
    }

    public override void SupportAbility()
    {
        if (acting)
            return;

        acting = true;

        controller.SetBool("Suport", true);
        DefUp();
        StartCoroutine("AbilityTime", "Suport");
    }
}
