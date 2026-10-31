using System.Reflection;
using BaseLib.Patches.Content;
using BaseLib.Patches.Localization;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;

namespace WoWTheSpire.WoWTheSpireCode.CustomProperties;

  
public class WoWKeywords {
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)] public static CardKeyword Holy;
    [CustomEnum] public static CardKeyword DoT;
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)] public static CardKeyword Desecrated;
    
    // Much thanks to Pikcube for explaining C# events
    static WoWKeywords() {
        DescriptionOverrides.CustomizeDescriptionPost += DescriptionOverrides_CustomizeDescriptionPost;
    }
    
    private static MethodInfo GetTextMethod => AccessTools.DeclaredMethod("CardKeywordExtensions:GetCardText");

    private static void DescriptionOverrides_CustomizeDescriptionPost(CardModel card, Creature? target, ref string description) {
        foreach (CardKeyword keyword in card.Keywords.Where(keyword => IsPurpleKeyword(keyword, card))) {
            string original = (string?)GetTextMethod.Invoke(null, [keyword]) ?? string.Empty;
            string newValue = original.Replace("gold]", "purple]");
            description = description.Replace(original, newValue);
        }
    }

    private static bool IsPurpleKeyword(CardKeyword keyword, CardModel card) => keyword == Desecrated;
}