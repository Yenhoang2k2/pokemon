using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move",menuName = "Pokemon/create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] private string name;
    [TextArea]
    [SerializeField] private string dicription;

    [SerializeField] private PokemonType type;
    [SerializeField] private int power;
    [SerializeField] private int accuracy;
    [SerializeField] private int pp;

    public string Name{
        get { return name; }
    }
    public string Dicription{
        get { return dicription; }
    }
    public PokemonType Type{
        get { return type; }
    }
    public int Power{
        get { return power; }
    }
    public int Pp{
        get { return pp; }
    }
    public int Accuracy{
        get { return accuracy; }
    }
}
