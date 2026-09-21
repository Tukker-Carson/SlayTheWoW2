using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class InnerWill() : PriestCard(1, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy) {
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<BlessedPower>(1),
        new ("Potency", 6)
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<BlessedPower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) { 
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target"); 
        await PowerCmd.Apply<BlessedPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            -DynamicVars[nameof(BlessedPower)].BaseValue,
            Owner.Creature,
            this);
        foreach (var power in play.Target.Powers.OfType<Powers.BaseDoT>()) 
            power.SetPotency(power.DynamicVars["Potency"].IntValue + DynamicVars["Potency"].IntValue);
    }
    
    protected override void OnUpgrade() => DynamicVars["Potency"].UpgradeValueBy(4);
}