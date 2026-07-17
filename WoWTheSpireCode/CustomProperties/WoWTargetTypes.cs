using BaseLib.Patches.Content;
using BaseLib.Patches.Features;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace WoWTheSpire.WoWTheSpireCode.CustomProperties;

public static class WoWTargetTypes {
     [CustomEnum] public static TargetType Any25PHpEnemies;
}

[HarmonyPatch(typeof(ModelDb), "Init")]
internal static class ModelDbTargetTypeInitPatch {
     [HarmonyPostfix]
     private static void RegisterTargetTypes() {
          // single targeting
          CustomTargetType.RegisterSingleTargetType(WoWTargetTypes.Any25PHpEnemies,
               target => target is { IsAlive: true, IsEnemy: true}  && target.CurrentHp <= target.MaxHp/4);
          // CustomTargetType.RegisterMultiTargetType(WoWTargetTypes.AllLowHpEnemies,
          //      target => target is { IsAlive: true, IsEnemy: true}  && target.CurrentHp <= target.MaxHp/2);
     }
}