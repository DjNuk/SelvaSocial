using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public Character[] members;
    [SerializeField]
    Enemy enemys;
    [SerializeField]
    GameObject actionBar;
    [SerializeField]
    GameObject sprite;

    public Character target;
    int enemyIndex = 0;

    int actionPoints = 10;
    bool speed = false;
    int time = 0;

    void Update ()
    {
        KeyInput();
        SelectAlly();
	}

    void KeyInput ()
    {
        Character[] c = enemys.members;

        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        if (hAxis <= -0.5f)
        {
            target = c[0];
            Enemy.select(0, actionPoints);
            enemyIndex = 0;
        }
        if (hAxis >= 0.5f)
        {
            if (c.Length >= 2)
            {
                target = c[1];
                Enemy.select(1, actionPoints);
                enemyIndex = 1;
            }
        }
        if (vAxis <= 0.5f)
        {
            if (c.Length >= 4)
            {
                target = c[4];
                Enemy.select(4, actionPoints);
                enemyIndex = 2;
            }
        }
        if (vAxis <= -0.5f)
        {
            if (c.Length >= 2)
            {
                target = c[2];
                Enemy.select(2, actionPoints);
                enemyIndex = 3;
            }
        }
    }

    void SelectAlly ()
    {
        if (target != null)
        {

            if (Input.GetButton("XboxX"))
            {
                if (!members[0].acting && !members[0].stun)
                {
                    time++;
                    if (time > 50)
                        members[0].glow.SetActive(true);
                }
                else
                    time = 0;

                return;
            }
            else if (Input.GetButtonUp("XboxX"))
            {
                if (!members[0].acting && !members[0].stun)
                    HoldButton(0);
            }
            if (Input.GetAxisRaw("XboxA") != 0)
            {
                /*acting = true;

                if (members.Length >= 2)
                    members[1].NormalAbility(target);*/
            }
            if (Input.GetAxisRaw("XboxB") != 0)
            {
               /* acting = true;

                if (members.Length >= 3)
                    members[2].NormalAbility(target);*/
            }
            if (Input.GetAxisRaw("XboxY") != 0)
            {
               /* acting = true;

                if (members.Length >= 4)
                    members[3].NormalAbility(target);*/
            }
        }
        else
        {
            if (Input.GetButtonDown("XboxX"))
            {
                if (members[0].supportAP <= actionPoints && !members[0].acting && !members[0].stun)
                {
                    members[0].SupportAbility();
                    UsePoints(members[0].supportAP);
                }
            }
            else if (Input.GetAxisRaw("XboxA") != 0)
            {
                if (members.Length >= 2)
                    members[1].SupportAbility();
            }
            else if (Input.GetAxisRaw("XboxB") != 0)
            {
                if (members.Length >= 3)
                    members[2].SupportAbility();
            }
            else if (Input.GetAxisRaw("XboxY") != 0)
            {
                if (members.Length >= 4)
                    members[3].SupportAbility();
            }
        }
    }

    void  HoldButton (int character)
    {
        if (time < 50)
        {
            if (members[character].normalAP <= actionPoints && !members[character].acting)
            {
                members[character].NormalAbility(target);
                Enemy.move(character, enemyIndex, actionPoints);
                UsePoints(members[character].normalAP);

                target = null;
            }
        }
        else
        {
            if (members[character].chargeAP <= actionPoints && !members[character].acting)
            {
                members[character].ChargeAbility(target);
                Enemy.move(character, enemyIndex, actionPoints);
                UsePoints(members[character].chargeAP);

                target = null;
                members[0].glow.SetActive(false);
            }
        }
        time = 0;
    }

    public void UsePoints (int points)
    {
        actionPoints = actionPoints - points;
        actionBar.transform.localPosition = new Vector3(actionBar.transform.localPosition.x - (0.5f * points), 0, 0);

        if (actionPoints + points == 10)
            StartCoroutine("ChargePoints");
    }

    IEnumerator ChargePoints ()
    {
        while (true)
        {
            if (speed)
                yield return new WaitForSeconds(1f);
            else
                yield return new WaitForSeconds(2f);

            actionPoints++;
            actionBar.transform.localPosition = new Vector3(actionBar.transform.localPosition.x + 0.5f, 0, 0);

            if (actionPoints == 10)
                break;
        }
        
    }

    public void SpeedUp()
    {
        StopCoroutine("Speed");
        StartCoroutine("Speed");
    }

    IEnumerator Speed()
    {
        speed = true;
        sprite.SetActive(true);

        yield return new WaitForSeconds(15);

        speed = false;
        sprite.SetActive(false);
    }
}
