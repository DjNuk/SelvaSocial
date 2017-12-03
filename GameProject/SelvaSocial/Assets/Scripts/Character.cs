using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public int hp;
    public int partyPos;

    public int normalAP;
    public int chargeAP;
    public int supportAP;

    public int currentAtack = 0;

    public bool acting = false;
    public bool stun = false;

    public bool defUp = false;
    public bool defDown = false;

    public Vector3 original = Vector3.zero;

    public Animator controller;
    public GameObject[] HurtBoxs;
    public GameObject highlight;

    public GameObject[] sprites;

    public Text live;

    public Character ()
    {

    }

    void Start()
    {
        original = transform.position;
        controller = transform.GetComponent<Animator>();
        live.text = "" + hp;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HurtBox")
            ReceiveDamage(other.GetComponentInParent<Character>().currentAtack);
    }

    public virtual void NormalAbility (Transform target)
    {

    }

    public virtual void ChargeAbility (Transform target)
    {

    }

    public virtual void SupportAbility ()
    {

    }

    public virtual void ReceiveDamage (int damage)
    {
        int original = damage;
        if (defUp)
            damage = damage - (int)(original * 0.4f);
        if(defDown)
            damage = damage + (int)(original * 0.4f);

        hp = hp - damage;
        live.text = "" + hp;

        if (hp <= 0)
            SceneManager.LoadScene(0);

        if (gameObject.GetComponentInParent<Enemy>())
        {
            if (gameObject.GetComponentInParent<Enemy>().target == null)
                StartCoroutine("Stun", 5);
            else
                StartCoroutine("Stun", 4);
        }
        else
        {
            if (gameObject.GetComponentInParent<Player>().target == null)
                StartCoroutine("Stun", 2);
            else
                StartCoroutine("Stun", 4);

        }
    }

    public virtual Transform GetTransform()
    {
        return this.transform;
    }

    public void DefUp ()
    {
        StopCoroutine("BuffDefUp");
        StartCoroutine("BuffDefUp");
    }

    public void DefDown()
    {
        StopCoroutine("BuffDefDown");
        StartCoroutine("BuffDefDown");
    }

    IEnumerator AbilityTime (string name)
    {
        yield return new WaitForSeconds(0.8f % controller.GetCurrentAnimatorStateInfo(0).length) ;

        controller.SetBool(name, false);
        transform.position = original;
        currentAtack = 0;
        acting = false;
    }

    IEnumerator Stun(int time)
    {
        stun = true;
        sprites[2].SetActive(true);

        yield return new WaitForSeconds(time);

        stun = false;
        sprites[2].SetActive(false);
    }

    IEnumerator BuffDefUp()
    {
        defUp = true;
        sprites[0].SetActive(true);

        yield return new WaitForSeconds(60);

        defUp = false;
        sprites[0].SetActive(false);
    }

    IEnumerator BuffDefDown()
    {
        defDown = true;
        sprites[1].SetActive(true);

        yield return new WaitForSeconds(60);

        defDown = false;
        sprites[1].SetActive(false);
    }
}
