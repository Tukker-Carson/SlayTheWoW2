using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.Powers;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;

public class PowerWordShield() : PriestCard(1, CardType.Skill, CardRarity.Basic, TargetType.AnyPlayer) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(7, ValueProp.Move), new PowerVar<WeakenedSoulPower>(1)];

    protected override bool IsPlayable => !Owner.Creature.HasPower<WeakenedSoulPower>();

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await CreatureCmd.GainBlock(play.Target ?? Owner.Creature, DynamicVars.Block, play);
        await PowerCmd.Apply<WeakenedSoulPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(WeakenedSoulPower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() => DynamicVars.Block.UpgradeValueBy(3);
}