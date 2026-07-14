// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Powers.PyrePower
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 97F10687-C306-4798-AB75-8B9F23F34DFB
// Assembly location: C:\Users\carso\SlayTheWoW2\WoWTheSpire\.godot\mono\temp\obj\Debug\PublicizedAssemblies\sts2.A4F5973794ACB7F77CCEA9E4C47067DA\sts2.dll
// XML documentation location: C:\Users\carso\SlayTheWoW2\WoWTheSpire\.godot\mono\temp\obj\Debug\PublicizedAssemblies\sts2.A4F5973794ACB7F77CCEA9E4C47067DA\sts2.xml

#nullable enable
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public sealed class HymnOfHopePower : WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override decimal ModifyMaxEnergy(Player player, decimal amount) {
        return player != Owner.Player ? amount : amount + Amount;
    }
}