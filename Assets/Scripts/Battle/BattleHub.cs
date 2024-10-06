using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHub : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text lvlText;
    [SerializeField] private HpBar hpBar;

    public void SetData(Pokemon pokemon)
    {
        nameText.text = pokemon.Base.Name;
        lvlText.text = "Lvl " + pokemon.Level.ToString();
        hpBar.SetHp((float)pokemon.Hp/pokemon.MaxHp);
    }
}
