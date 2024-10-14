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
    [SerializeField] private List<LeanableMove> leanableMoves;

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
    public List<LeanableMove> LeanableMoves{
        get { return leanableMoves; }
    }
}
[System.Serializable]
public class LeanableMove
{
    public MoveBase moveBase;
    public  int level;
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

public class TypeChart
{
    private static float[][] _chart =
    {
        //                      NOR  FIR  WAT  ELE  GRA  ICE  FIG  POI
        /* NOR */ new float[] { 1f,  1f,  1f,  1f,  1f,  1f,  1f,  1f },
        /* FIR */ new float[] { 1f,  0.5f,0.5f,1f,  2f,  2f,  1f,  1f },
        /* WAT */ new float[] { 1f,  2f,  0.5f,2f,  0.5f,1f,  1f,  1f },
        /* GRA */ new float[] { 1f,  1f,  2f,  0.5f,0.5f,2f,  1f,  1f },
        /* ICE */ new float[] { 1f,  0.5f,2f,  2f,  0.5f,1f,  1f,  0.5f },
        /* FIG */ new float[] { 1f,  1f,  1f,  1f,  2f,  1f,  1f,  1f }
    };

    public static float GetEffectiveness( PokemonType attackType, PokemonType defencetype)
    {
        if (attackType == PokemonType.None || defencetype == PokemonType.None)
            return 1f;
        int row = (int)attackType - 1;
        int col = (int)defencetype - 1;
        return _chart[row][col];
    }
}
