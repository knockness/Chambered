using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [Header ("Constants")]
    public int iframes;
    public float health;

    [Header ("State Booleans")]
    public bool isDamaged;

    [Header("Check Variables")]
    public float damageChange;


    void Awake()
    {
        isDamaged = true;
        damageChange = health;
    }

    void Update()
    {
        if (damageChange != health)
        {
            damageChange = health;
            StartCoroutine(ImmunityTime(iframes));
        }

        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator ImmunityTime(int iframes)
    {
        for (int i = 0; i < iframes; i++)
        {
            isDamaged = false;
            yield return new WaitForFixedUpdate();
        }

        isDamaged = true;
        yield break;
    }
}