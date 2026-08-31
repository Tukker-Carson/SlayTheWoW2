using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class PrayerOfHealing() : PriestCard(-1, CardType.Skill, CardRarity.Uncommon, TargetType.AllAllies) {
    private Decimal HealAmount(Creature target) {
        var allyCount = CombatState!.Allies.Count(a => a is { IsPlayer: true, IsAlive: true });
        var energyX = ResolveEnergyXValue();
        return target == Owner.Creature
            ? energyX  * DynamicVars.Heal.BaseValue / allyCount
            : energyX * DynamicVars.Heal.BaseValue / allyCount + energyX * DynamicVars.Heal.BaseValue % allyCount;
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new WoWHealVar(5, ValueProp.Move)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy];
    protected override bool HasEnergyCostX => true;
    public override bool CanBeGeneratedInCombat => false;
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(CombatState);
        var count = ResolveEnergyXValue()*DynamicVars.Heal.BaseValue;
        if (count <= 0) return;
        foreach (var creature in CombatState.Allies.Where(a => a is {IsPlayer: true, IsAlive: true})) 
            await WoWCmd.Heal(creature,Owner.Creature, HealAmount(creature),ValueProp.Unpowered, play);
    }
    
    protected override void OnUpgrade() => DynamicVars["WoWHeal"].UpgradeValueBy(2);
}