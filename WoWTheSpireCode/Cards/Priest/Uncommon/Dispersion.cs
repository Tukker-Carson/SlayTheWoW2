using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class Dispersion() : PriestCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<DispersionPower>(1), new BoolVar("Playable", true)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override bool IsPlayable => Convert.ToBoolean(DynamicVars["Playable"].BaseValue);

    public override Task AfterAttack(PlayerChoiceContext choiceContext, AttackCommand command) {
        DynamicVars["Playable"].BaseValue = 0;
        return base.AfterAttack(choiceContext, command);
    }

    public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants) {
        DynamicVars["Playable"].BaseValue = 1;
        return base.AfterSideTurnEnd(choiceContext, side, participants);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<DispersionPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(DispersionPower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() => RemoveKeyword(CardKeyword.Exhaust);
}