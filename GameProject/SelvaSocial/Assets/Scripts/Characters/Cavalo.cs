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

    public override void NormalAbility(Transform target)
    {
        if (acting)
            return;

        acting = true;

        transform.position = new Vector3(target.position.x + 0.8f, target.position.y, target.position.z);
        controller.SetBool("Atack",true);
        HurtBoxs[0].SetActive(true);
        currentAtack = 15;

        StartCoroutine("AbilityTime", "Atack");
    }

    public override void ChargeAbility(Transform target)
    {
        if (acting)
            return;

        acting = true;

        transform.position = new Vector3(target.position.x + 0.8f, target.position.y, target.position.z);
        controller.SetBool("Charge", true);
        target.gameObject.GetComponent<Character>().DefDown();

        StartCoroutine("AbilityTime", "Charge");
    }

    public override void SupportAbility()
    {
        if (acting)
            return;

        acting = true;

        controller.SetBool("Suport", true);
        gameObject.GetComponentInParent<Player>().SpeedUp();
        StartCoroutine("AbilityTime", "Suport");
    }
}
