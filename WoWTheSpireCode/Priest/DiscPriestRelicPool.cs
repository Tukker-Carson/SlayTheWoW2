using BaseLib.Abstracts;
using Godot;
using WoWTheSpire.WoWTheSpireCode.Extensions;

namespace WoWTheSpire.WoWTheSpireCode.Priest;

public class DiscPriestRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => DiscPriest.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}