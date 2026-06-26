using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.Powers;

namespace WoWTheSpire.WoWTheSpireCode.Cards;

public class PowerWordShield() : PriestCard(1, CardType.Skill, CardRarity.Basic, TargetType.AnyAlly) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(5, ValueProp.Move), new PowerVar<WeakenedSoulPower>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await CreatureCmd.GainBlock(play.Target, DynamicVars.Block, play);
        await PowerCmd.Apply<WeakenedSoulPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(WeakenedSoulPower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() => DynamicVars.Block.UpgradeValueBy(3);
}