using UnityEngine;

public class SoulModel
{
    public readonly SoulReincornationType[] ReincornationTypesResult;
    public readonly SoulType[] SoulTypesResult;
    public readonly string Phrase;
    public readonly Color Color;

    public SoulModel(SoulData type)
    {
        ReincornationTypesResult = type.ReincornationTypesResult;
        SoulTypesResult = type.SoulTypesResult;
        Phrase = type.Phrases[Random.Range(0, type.Phrases.Length)];
        Color = type.Colors[Random.Range(0, type.Colors.Length)];
    }
}