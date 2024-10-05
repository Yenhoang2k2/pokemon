using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon",menuName = "Pokemon/create Pokemon")]
public class PokemonBase : ScriptableObject
{
    [SerializeField] private string name;
    
    [TextArea]
    [SerializeField] private string dicription;

    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;

    [SerializeField] private PokemonType type1;
    [SerializeField] private PokemonType type2;

    [SerializeField] private int maxHp;
    [SerializeField] private int attack;
    [SerializeField] private int defence;
    [SerializeField] private int spAttack;
    [SerializeField] private int spDefence;
    [SerializeField] private int speed;

    public string Name{
        get { return name; }
    }
    public string Dicription{
        get { return dicription; }
    }
    public Sprite FrontSprite{
        get { return frontSprite; }
    }
    public Sprite BackSprite{
        get { return backSprite; }
    }
    public PokemonType Type1{
        get { return type1; }
    }
    public PokemonType Type2{
        get { return type2; }
    }
    public int MaxHp{
        get { return maxHp; }
    }
    public int Attack{
        get { return attack; }
    }
    public int Defence{
        get { return defence; }
    }
    public int SpDefence{
        get { return spDefence; }
    }
    public int SpAttack{
        get { return spAttack; }
    }
    public int Speed{
        get { return speed; }
    }
}

public enum PokemonType
{
    None,
    Nomal,
    Fire,
    Water,
    Electric,
    Grass,
    Ice,
    Fighting,
    Posion,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon
}
