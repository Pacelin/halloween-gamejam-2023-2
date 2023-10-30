using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
	[System.Serializable]
	public class SoulScore
	{
		public SoulType SoulType;
		public int Score;
	}
	[System.Serializable]
	public class ReincarnationScore
	{
		public SoulReincornationType SoulType;
		public int Score;
	}

	public SoulScore[] SoulScores;
	public ReincarnationScore[] ReincarnationScores;
	public Color LiquidColor;
	[Range(0f, 1f)]
	public float LiquidAmount;
}