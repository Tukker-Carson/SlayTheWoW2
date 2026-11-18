using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;


namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class VoidShift() : PriestCard(1, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new("Amount", 1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await WoWCmd.Sanctify(choiceContext, this, CombatState!, 1, DynamicVars["Amount"].IntValue);
    }

    protected override void OnUpgrade() => DynamicVars["Amount"].UpgradeValueBy(2);
}