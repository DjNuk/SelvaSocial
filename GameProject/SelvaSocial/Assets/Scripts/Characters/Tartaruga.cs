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

    public override void NormalAbility(Character target)
    {
        if (acting)
            return;

        acting = true;
        
        controller.SetBool("Atack", true);
        HurtBoxs[0].gameObject.SetActive(true);
        target.ReceiveDamage(20); 

        StartCoroutine("AbilityTime", "Atack");
        source.PlayOneShot(ap1Sound);
    }

    public override void ChargeAbility(Character target)
    {
        if (acting)
            return;

        acting = true;

        controller.SetBool("Charge", true);
        HurtBoxs[1].SetActive(true);
        target.ReceiveDamage(30);

        StartCoroutine("AbilityTime", "Charge");
        source.PlayOneShot(ap2Sound);
    }

    public override void SupportAbility()
    {
        if (acting)
            return;

        acting = true;

        controller.SetBool("Suport", true);
        DefUp();
        StartCoroutine("AbilityTime", "Suport");
        source.PlayOneShot(ap3Sound);
    }
}
