using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class LeapOfFaith() : PriestCard(0, CardType.Skill, CardRarity.Rare, TargetType.AllAllies) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new WoWHealVar(20, ValueProp.Move),
        new ("MaxHpLoss", 2)
    ];
    public override bool CanBeGeneratedInCombat => false;
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy];

    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        foreach (var ally in CombatState!.Allies)
            await WoWCmd.Heal(ally, Owner.Creature, (WoWHealVar)DynamicVars["WoWHeal"], play);
        await CreatureCmd.LoseMaxHp(choiceContext, Owner.Creature, DynamicVars["MaxHpLoss"].BaseValue, true);
    }
    
    protected override void OnUpgrade() => DynamicVars["MaxHpLoss"].UpgradeValueBy(-1);
}