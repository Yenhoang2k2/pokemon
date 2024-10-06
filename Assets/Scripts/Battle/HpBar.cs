using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] private GameObject health;

    private void Start()
    {
        health.transform.localScale = new Vector3(0.5f, 1f);
    }

    public void SetHp(float hpNormalize)
    {
        health.transform.localScale = new Vector3(hpNormalize, 1f);
    }
}
