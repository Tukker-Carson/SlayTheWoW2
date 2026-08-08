using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class Phantasm() : PriestCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<BlessedPower>(2),
        new PowerVar<BufferPower>(1)
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<BufferPower>(),
        HoverTipFactory.FromPower<BlessedPower>()
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<BlessedPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            -DynamicVars[nameof(BlessedPower)].BaseValue,
            Owner.Creature,
            this);
        await PowerCmd.Apply<BufferPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(BufferPower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() {
        DynamicVars.Power<BlessedPower>().UpgradeValueBy(1);
        DynamicVars.Power<BufferPower>().UpgradeValueBy(1);
    }
}