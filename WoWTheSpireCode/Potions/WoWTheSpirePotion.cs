using BaseLib.Abstracts;
using BaseLib.Utils;
using WoWTheSpire.WoWTheSpireCode.Character;

namespace WoWTheSpire.WoWTheSpireCode.Potions;

[Pool(typeof(WoWTheSpirePotionPool))]
public abstract class WoWTheSpirePotion : CustomPotionModel;