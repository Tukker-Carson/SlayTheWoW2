using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class AtonementPower() : WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new("SpecAmount", 25)];

    public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer,
        DamageResult result, ValueProp props, Creature target, CardModel? cardSource) {
        if (dealer != Owner || cardSource is null || !cardSource.Keywords.Contains(WoWKeywords.Holy) 
            || props == ValueProp.Unpowered || result.UnblockedDamage < 1)
        {
            MainFile.Logger.Info("Owner: " + Owner + "; Dealer: " + dealer + "; CardSource: " + (cardSource is null?"null":cardSource.Keywords.Contains(WoWKeywords.Holy)) + "; Props:  " + props);
            return;
        }
        var lowHealth = 0;
        var newOwner = Owner;
        foreach (var creature in CombatState.GetTeammatesOf(Owner)
                     .Where(c => c is { IsAlive: true, IsPlayer: true })) {
            if (creature.CurrentHp <= lowHealth) continue;
            lowHealth = creature.CurrentHp;
            newOwner = creature;
        }
        await WoWCmd.Heal(newOwner, Owner, (decimal)result.UnblockedDamage*Amount/4, ValueProp.Move, null);
    }

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount,
        Creature? applier, CardModel? cardSource) {
        DynamicVars["SpecAmount"].BaseValue = amount*25;
        return Task.CompletedTask;
    }
}