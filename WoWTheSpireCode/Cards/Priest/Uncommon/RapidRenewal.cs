using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class RapidRenewal() : PriestCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<RenewPower>(2), new IntVar("Potency", 7)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<RenewPower>()];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    public override bool CanBeGeneratedInCombat => false;
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        if (!Owner.HasPower<RenewPower>())
            await PowerCmd.Apply<RenewPower>(choiceContext, Owner.Creature,
                DynamicVars[nameof(RenewPower)].BaseValue, Owner.Creature, this);
        else Owner.Creature.GetPower<RenewPower>()!.DynamicVars.Heal.BaseValue *= 2;
    }
    
    protected override void OnUpgrade() => RemoveKeyword(CardKeyword.Exhaust);
}