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

    public DameDetails TakeDame(Move move,Pokemon attacker)
    {
        float critical = 1f;
        if (Random.value * 100 <= 6.5f)
            critical = 2;
        float type = TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type1) *
                     TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type2);
        DameDetails dameDetails = new DameDetails()
        {
            fainted = false,
            critical = critical,
            type = type,

        };
        float modifiers = Random.Range(0.85f, 1f) * type;
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attacker.Attack / Defence) + 2;
        int dame = Mathf.FloorToInt(d * modifiers);
        Hp -= dame;
        if (Hp <= 0)
        {
            Hp = 0;
            dameDetails.fainted = true;
        }

        return dameDetails;
    }

    public Move GetRandom()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }
}

public class DameDetails
{
    public bool fainted { get; set; }
    public float critical { get; set; }
    public float type { get; set; }
}

