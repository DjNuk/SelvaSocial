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
    public AudioSource source;

    public AudioClip ap1Sound;
    public AudioClip ap2Sound;
    public AudioClip ap3Sound;

    public GameObject[] HurtBoxs;
    public GameObject highlight;

    public GameObject[] sprites;

    public Text live;
    public GameObject glow;

    public Character ()
    {

    }

    void Start()
    {
        original = transform.position;
        controller = transform.GetComponent<Animator>();
        source = transform.GetComponent<AudioSource>();
        live.text = "" + hp;
    }

    public virtual void NormalAbility (Character target)
    {

    }

    public virtual void ChargeAbility (Character target)
    {

    }

    public virtual void SupportAbility ()
    {

    }

    public virtual void ReceiveDamage (int damage)
    {
        controller.SetBool("Hit", true);
        StartCoroutine("AbilityTime", "Hit");

        int original = damage;
        if (defUp)
            damage = damage - (int)(original * 0.4f);
        if(defDown)
            damage = damage + (int)(original * 0.4f);

        hp = hp - damage;
        live.text = "" + hp;

        if (hp <= 0)
            StartCoroutine("Dead");

        if (gameObject.GetComponentInParent<Enemy>())
        {
            if (gameObject.GetComponentInParent<Enemy>().target == null)
                StartCoroutine("Stun", 2);
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
        acting = false;
    }

    IEnumerator Stun(int time)
    {
        stun = true;
        sprites[2].SetActive(true);

        yield return new WaitForSeconds(time);

        stun = false;
        controller.SetBool("Hit", false);
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

    IEnumerator Dead()
    {
        controller.SetBool("Dead", true);

        yield return new WaitForSeconds(4);

        Player player = transform.root.GetComponent<Player>();
        if (player == null)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(3);
    }
}
