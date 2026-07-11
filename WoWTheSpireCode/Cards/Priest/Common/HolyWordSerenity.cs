using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;

public class HolyWordSerenity() : PriestCard(1, CardType.Skill, CardRarity.Common, CustomTargetType.Everyone) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new HealVar(3),
        new DamageVar(6, ValueProp.Unpowered),
        new PowerVar<HolyWordSerenityPower>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<HolyWordSerenityPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(HolyWordSerenityPower)].BaseValue,
            Owner.Creature,
            this);
    }


    protected override void OnUpgrade() => DynamicVars[nameof(HolyWordSerenityPower)].UpgradeValueBy(1);
}