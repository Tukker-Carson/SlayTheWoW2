using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class BodyAndSoul() : PriestCard(2, CardType.Power, CardRarity.Uncommon, TargetType.Self) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<BodyAndSoulPower>(1)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<BlessedPower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        await PowerCmd.Apply<BodyAndSoulPower>(new ThrowingPlayerChoiceContext(), 
            Owner.Creature,
            DynamicVars[nameof(BodyAndSoulPower)].BaseValue,
            Owner.Creature,
            null);
    }
    
    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}