using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHub : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text lvlText;
    [SerializeField] private HpBar hpBar;
    private Pokemon _pokemon;

    public void SetData(Pokemon pokemon)
    {
        _pokemon = pokemon;
        nameText.text = pokemon.Base.Name;
        lvlText.text = "Lvl " + pokemon.Level;
        hpBar.SetHp((float)pokemon.Hp/pokemon.MaxHp);
    }

    public IEnumerator UpdateHp()
    {
        yield return hpBar.SetHpSmooth((float)_pokemon.Hp/_pokemon.MaxHp);
    }
}
