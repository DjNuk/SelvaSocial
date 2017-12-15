using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavalo : Character
{
    [SerializeField]
    GameObject sup;

    void Update()
    {
        if (!acting)
            HurtBoxs[0].gameObject.SetActive(false);
    }

    public override void NormalAbility(Character target)
    {
        if (acting)
            return;

        acting = true;

        controller.SetBool("Atack",true);
        HurtBoxs[0].SetActive(true);
        target.ReceiveDamage(15);

        StartCoroutine("AbilityTime", "Atack");
        source.PlayOneShot(ap1Sound);
    }

    public override void ChargeAbility(Character target)
    {
        if (acting)
            return;

        acting = true;
        

        controller.SetBool("Charge", true);
        target.DefDown();

        StartCoroutine("AbilityTime", "Charge");
        source.PlayOneShot(ap2Sound);
    }

    public override void SupportAbility()
    {
        if (acting)
            return;

        acting = true;

        controller.SetBool("Suport", true);
        gameObject.GetComponentInParent<Player>().SpeedUp();
        StartCoroutine("AbilityTime", "Suport");
        source.PlayOneShot(ap3Sound);
    }
}
