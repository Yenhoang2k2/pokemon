using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    public PokemonBase Base { get; set; }
    public int Level { get; set; }
    public int Hp { get; set; }
    public List<Move> Moves { get; set; }

    public Pokemon(PokemonBase pbase,int plevel)
    {
        Base = pbase;
        Level = plevel;
        Hp = MaxHp;
        Moves = new List<Move>();
        foreach (var move in Base.LeanableMoves)
        {
            if(move.level <= Level)
                Moves.Add(new Move(move.moveBase));
            if(Moves.Count >= 4)
                break;
        }
    }

    public int Attack{
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; }
    }
    public int Defence{
        get { return Mathf.FloorToInt((Base.Defence * Level) / 100f) + 5; }
    }
    public int SpAttack{
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5; }
    }
    public int SpDenfence{
        get { return Mathf.FloorToInt((Base.SpDefence * Level) / 100f) + 5; }
    }
    public int Speed{
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; }
    }
    public int MaxHp{
        get { return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10; }
    }
}
