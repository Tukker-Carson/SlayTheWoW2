using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Relics;

public class CloakOfTheDisciple : WoWTheSpireRelic, IWoWHealListener {
    public override RelicRarity Rarity => RelicRarity.Starter;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Block)];

    public async Task AfterHeal(Creature target, Creature source, decimal amount, decimal overheal, ValueProp props, CardPlay? cardPlay) {
        if (source != Owner.Creature) return;
        await CreatureCmd.GainBlock(target, amount+overheal, ValueProp.Unpowered, cardPlay);
    }
}