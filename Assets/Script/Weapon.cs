using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Hammer, Range, Bomb};
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public SphereCollider StunArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;
    public GameObject RockAttack;

    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        if (type == Type.Hammer)
        {
            StopCoroutine("Stun");
            StartCoroutine("Stun");
        }
        else if (type == Type.Range)
        {
            StartCoroutine("Shot");
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.85f);
        meleeArea.enabled = false;
        RockAttack.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        RockAttack.gameObject.SetActive(false);
        trailEffect.enabled = false;
    }

    IEnumerator Stun()
    {
        yield return new WaitForSeconds(0.1f);
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.85f);
        StunArea.enabled = true;
        RockAttack.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        RockAttack.gameObject.SetActive(false);
        trailEffect.enabled = false;
        StunArea.enabled = false;
    }

    IEnumerator Shot()
    {
        // #1.√—æÀ πﬂªÁ
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 20;

        yield return null;

        // #2. ≈∫«« πË√‚
        GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = intantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }

}
