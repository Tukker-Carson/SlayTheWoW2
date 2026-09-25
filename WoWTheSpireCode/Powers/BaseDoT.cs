using BaseLib.Hooks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Platform;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Powers;

public abstract class BaseDoT : WoWTheSpirePower {
    public Color ForecastColor = new ("#78104C");
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override PowerInstanceType InstanceType => PowerInstanceType.InstancedPerApplier;
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(0, ValueProp.Unpowered),
        new StringVar("Applier"),
        new BoolVar("ApplierIsYou", true),
        new IntVar("Potency", 0)
    ];

    public void SetPotency(int potency) {
        AssertMutable();
        DynamicVars["Potency"].BaseValue = potency;
    }

    public void UpdatePotency(Creature source, Decimal potency) {
        AssertMutable();
        Applier = source;
        DynamicVars.Damage.BaseValue = Math.Max(DynamicVars["Potency"].BaseValue, potency);
        DynamicVars["Potency"].BaseValue = DynamicVars.Damage.BaseValue;
        DynamicVars.Damage.BaseValue = DynamicVars["Potency"].BaseValue + (Applier is not null && Applier.HasPower<ShadowformPower>() && Applier.HasPower<ShadowyApparitionPower>()?
            Applier!.GetPowerAmount<ShadowyApparitionPower>():0);
    }
    
    protected Task<IEnumerable<DamageResult>> Tick(PlayerChoiceContext choiceContext) {
        return WoWCmd.DotTick(choiceContext, Owner, Applier!, DynamicVars["Potency"].BaseValue);
    }
    
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants) {
        if (!participants.Contains(Owner)) return;
        await Tick(choiceContext);
        await PowerCmd.Decrement(this);
    }
    
    public override Task AfterApplied(Creature? applier, CardModel? cardSource) {
        ((StringVar)DynamicVars["Applier"]).StringValue = Applier!.Player!.NetId == 1 ? "You" : PlatformUtil.GetPlayerName(RunManager.Instance.NetService.Platform, Applier!.Player!.NetId);
        ((BoolVar)DynamicVars["ApplierIsYou"]).BaseValue = Applier!.Player!.NetId == 1 ? 1 : 0;
        
        return Task.CompletedTask;
    }

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource) {
        if (power != this && power is not ShadowyApparitionPower && power is not ShadowformPower) return;
        Amount = (int)Math.Max(amount, Amount - amount);
        if (cardSource is not null && power == this && cardSource.DynamicVars.Values.Any(i => i.Name == "Potency"))
            UpdatePotency(cardSource.Owner.Creature, cardSource.DynamicVars["Potency"].BaseValue);
        DynamicVars.Damage.BaseValue = DynamicVars["Potency"].BaseValue + (Applier is not null && Applier.HasPower<ShadowformPower>() 
            && Applier.HasPower<ShadowyApparitionPower>()?Applier!.GetPowerAmount<ShadowyApparitionPower>():0);
    }
    
    public override IEnumerable<HealthBarForecastSegment> GetHealthBarForecastSegments(HealthBarForecastContext context) {
        return [new HealthBarForecastSegment(
            (int)WoWCmd.ResolveDotTickAmount(Owner, Applier!, DynamicVars["Potency"].BaseValue), 
            ForecastColor, HealthBarForecastDirection.FromRight, 99)
        ];
    }
}