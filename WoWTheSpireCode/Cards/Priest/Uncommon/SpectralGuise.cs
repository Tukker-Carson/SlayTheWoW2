using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class SpectralGuise() : PriestCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<SpectralGuisePower>(1)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<FearPower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        await PowerCmd.Apply<SpectralGuisePower>(new ThrowingPlayerChoiceContext(), 
            Owner.Creature,
            DynamicVars[nameof(SpectralGuisePower)].BaseValue,
            Owner.Creature,
            null);
    }
    
    protected override void OnUpgrade() => DynamicVars[nameof(SpectralGuisePower)].UpgradeValueBy(1);
}