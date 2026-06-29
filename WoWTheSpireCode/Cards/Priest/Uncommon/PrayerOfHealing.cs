using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class PrayerOfHealing() : PriestCard(-1, CardType.Skill, CardRarity.Uncommon, TargetType.AllAllies) {
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new HealVar(5)];
    protected override bool HasEnergyCostX => true;
    public override bool CanBeGeneratedInCombat => false;
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(CombatState);
        var count = ResolveEnergyXValue()*DynamicVars.Heal.BaseValue;
        if (count <= 0) return;
        foreach (var player in CombatState.Allies) await CreatureCmd.Heal(player, count/CombatState.Allies.Count);
    }
    
    protected override void OnUpgrade() => DynamicVars.Heal.UpgradeValueBy(2);
}