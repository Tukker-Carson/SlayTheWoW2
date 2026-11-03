using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class DevouringPlague() : PriestCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<DevouringPlaguePower>(2),
        new CalculationBaseVar(2),
        new CalculationExtraVar(3),
        new CalculatedVar("Potency").WithMultiplier((card, _) => GetDesecrated(card.Owner))
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromKeyword(WoWKeywords.Desecrated)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.DoT];
    public override bool CanBeGeneratedInCombat => false;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        var desecratedAmount = ((CalculatedVar)DynamicVars["Potency"]).Calculate(play.Target);
        if (desecratedAmount == 0) return;
        var plague = await PowerCmd.Apply<DevouringPlaguePower>(choiceContext,
            play.Target,
            DynamicVars[nameof(DevouringPlaguePower)].IntValue,
            Owner.Creature,
            this);
        plague?.UpdatePotency(Owner.Creature, desecratedAmount);
        
    }

    private static decimal GetDesecrated(Player owner) {
        return owner.PlayerCombatState!.AllCards.Count(c =>
            c.Keywords.Contains(WoWKeywords.Desecrated) && c.Pile!.Type != PileType.Exhaust);
    }
    
    protected override void OnUpgrade() => DynamicVars["Potency"].UpgradeValueBy(2);
}