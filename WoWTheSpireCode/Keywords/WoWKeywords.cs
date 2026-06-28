using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace WoWTheSpire.WoWTheSpireCode.Keywords;

public class WoWKeywords {
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)] public static CardKeyword Holy;
    [CustomEnum] public static CardKeyword DoT;
}