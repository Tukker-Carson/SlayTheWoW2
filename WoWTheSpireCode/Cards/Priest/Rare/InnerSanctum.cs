using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class InnerSanctum() : PriestCard(0, CardType.Skill, CardRarity.Rare, TargetType.Self) {
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<BlessedPower>(1),
        new BlockVar(15, ValueProp.Move)
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<BlessedPower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) { 
        await PowerCmd.Apply<BlessedPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            -DynamicVars[nameof(BlessedPower)].BaseValue,
            Owner.Creature,
            this);
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
    }
    
    protected override void OnUpgrade() => DynamicVars.Block.UpgradeValueBy(8);
}