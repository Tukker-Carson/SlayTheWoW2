using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class HolyNova() : PriestCard(2, CardType.Attack, CardRarity.Uncommon, CustomTargetType.Everyone) {
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ("GenericAmount", 6)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy, CardKeyword.Exhaust];
    public override bool CanBeGeneratedInCombat => false;
    public override int MaxUpgradeLevel => 999;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(CombatState);
        foreach (var ally in CombatState.Allies)  
            await CreatureCmd.Heal(ally, DynamicVars["GenericAmount"].BaseValue/CombatState.Creatures.Count);
        await DamageCmd.Attack(DynamicVars["GenericAmount"].BaseValue/CombatState.Creatures.Count).FromCard(this).TargetingAllOpponents(CombatState)
            .WithHitFx("vfx/vfx_attack_blunt", null, "heavy_attack.mp3").Execute(choiceContext);
    }

    protected override void OnUpgrade() => DynamicVars["GenericAmount"].UpgradeValueBy(DynamicVars["GenericAmount"].BaseValue);
}