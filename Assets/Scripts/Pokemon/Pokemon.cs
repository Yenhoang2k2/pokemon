using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    private PokemonBase _base;
    private int level;
    public int Hp { get; set; }
    public List<Move> Moves { get; set; }

    public Pokemon(PokemonBase pbase,int plevel)
    {
        _base = pbase;
        level = plevel;
        Hp = MaxHp;
        Moves = new List<Move>();
        foreach (var move in _base.LeanableMoves)
        {
            if(move.level <= level)
                Moves.Add(new Move(move.moveBase));
            if(Moves.Count >= 4)
                break;
        }
    }

    public int Attack{
        get { return Mathf.FloorToInt((_base.Attack * level) / 100f) + 5; }
    }
    public int Defence{
        get { return Mathf.FloorToInt((_base.Defence * level) / 100f) + 5; }
    }
    public int SpAttack{
        get { return Mathf.FloorToInt((_base.SpAttack * level) / 100f) + 5; }
    }
    public int SpDenfence{
        get { return Mathf.FloorToInt((_base.SpDefence * level) / 100f) + 5; }
    }
    public int Speed{
        get { return Mathf.FloorToInt((_base.Speed * level) / 100f) + 5; }
    }
    public int MaxHp{
        get { return Mathf.FloorToInt((_base.MaxHp * level) / 100f) + 10; }
    }
}
