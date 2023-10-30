using System.Collections.Generic;

public static class SoulTypePresenter
{
	public static Dictionary<SoulType, string> SoulTypes = new Dictionary<SoulType, string>()
	{
		{ SoulType.Human, "человека" },
		{ SoulType.Animal, "животного" },
		{ SoulType.Plant, "растения" },
		{ SoulType.Fish, "рыбы" },
		{ SoulType.Bug, "насекомого" },
		{ SoulType.Zombie, "зомби" },
	};
	public static Dictionary<SoulReincornationType, string> SoulReincornationTypes = new Dictionary<SoulReincornationType, string>()
	{
		{ SoulReincornationType.Eater, "чревоугодником" },
		{ SoulReincornationType.Artist, "художником" },
		{ SoulReincornationType.Sheep, "овцой" },
		{ SoulReincornationType.Vampire, "вампиром" },
	};
}