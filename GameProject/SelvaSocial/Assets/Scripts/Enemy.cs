using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    public delegate void SelectingEnemy(int index, int actionPoints);
    public static SelectingEnemy select;

    public delegate void FinishingMove(int playerChar, int enemyChar, int actionPoints);
    public static FinishingMove move;

    [SerializeField]
    public Character[] members;
    [SerializeField]
    Player enemys;

    public Character target;

    List<Vector3> playerMovements = new List<Vector3>();
    int actionPoints = 10;

    float time = 0;

    void Start()
    {
        for (int i = 0; i < members.Length - 1; i++)
        {
            members[i].partyPos = i;
        }
    }

    void Update()
    {
        if (time == 0)
            time = Time.time;

        if (Time.time - time >= 8f)
        {
            int ability = Random.Range(1, 4);

            if (!members[0].acting)
            { 
                switch (ability)
                {
                    case 1:
                        if (members[0].normalAP <= actionPoints && !members[0].stun)
                        {
                            members[0].NormalAbility(enemys.members[0]);
                            actionPoints = actionPoints - members[0].normalAP;
                            if (actionPoints + members[0].normalAP == 10)
                                StartCoroutine("ChargePoints");
                        }
                        break;
                    case 2:
                        if (members[0].chargeAP <= actionPoints && !members[0].stun)
                        {
                            members[0].ChargeAbility(enemys.members[0]);
                            actionPoints = actionPoints - members[0].chargeAP;
                            if (actionPoints + members[0].chargeAP == 10)
                                StartCoroutine("ChargePoints");
                        }
                        break;
                    case 3:
                        if (!members[0].defUp && members[0].supportAP <= actionPoints && !members[0].stun)
                        {
                            members[0].SupportAbility();
                            actionPoints = actionPoints - members[0].supportAP;
                            if (actionPoints + members[0].supportAP == 10)
                                StartCoroutine("ChargePoints"); ;
                        }
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
            }
            time = Time.time;
        }
    }

    void OnEnable()
    {
        select += PlayerMovement;
        move += AddMove;
    }

    public void PlayerMovement (int index, int actionPoints)
    {
        if (playerMovements == null)
            return;

        for (int i = 0; i <= playerMovements.Count - 1; i++)
        {
            if (playerMovements[i].x == index && playerMovements[i].y == actionPoints)
            {
                target = enemys.members[(int)playerMovements[i].z];
                Counter();
                break;
            }
        }

        members[index].highlight.SetActive(true);
    }

    public void AddMove(int playerChar, int enemyChar, int actionPoints)
    {
        bool hasAdd = false;

        if (playerMovements != null)
        {
            for (int i = 0; i < playerMovements.Count - 1; i++)
            {
                if (playerMovements[i].x == enemyChar && playerMovements[i].y == actionPoints && playerMovements[i].z == playerChar)
                {
                    hasAdd = true;
                    break;
                }
            }
        }
        if (!hasAdd)
            playerMovements.Add(new Vector3(enemyChar, actionPoints, playerChar));

        members[enemyChar].highlight.SetActive(false);
    }

    public void Counter ()
    {
        int index = Random.Range(0, members.Length - 1);
        if (members[index].normalAP <= actionPoints && !members[index].stun && !members[index].acting)
        {
            members[index].NormalAbility(target);
            actionPoints = actionPoints - members[index].normalAP;

            if (actionPoints + members[index].normalAP == 10)
                StartCoroutine("ChargePoints"); ;
        }
    }

    IEnumerator ChargePoints()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.4f);

            actionPoints++;

            if (actionPoints == 10)
                break;
        }

    }

    void OnDisable()
    {
        select -= PlayerMovement;
        move -= AddMove;
    }
}
