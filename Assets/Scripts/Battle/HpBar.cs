using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] private GameObject health;

    private void Start()
    {
        health.transform.localScale = new Vector3(1f, 1f);
    }

    public void SetHp(float hpNormalize)
    {
        health.transform.localScale = new Vector3(hpNormalize, 1f);
    }

    public IEnumerator SetHpSmooth(float newHp)
    {
        float curHp = health.transform.localScale.x;
        float atmChange = curHp - newHp;
        while (curHp - newHp >= Mathf.Epsilon)
        {
            curHp -= atmChange * Time.deltaTime;
            health.transform.localScale = new Vector3(curHp,1f);
            yield return null;
        }

        health.transform.localScale = new Vector3(newHp, 1f);
    }
}
