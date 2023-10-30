using UnityEngine;

[CreateAssetMenu]
public class SoulData : ScriptableObject
{
	public SoulType[] SoulTypesResult;
	public SoulReincornationType[] ReincornationTypesResult;
	[Space]
	[Multiline] public string[] Phrases;
	public Color[] Colors;
}