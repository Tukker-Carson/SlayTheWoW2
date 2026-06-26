using BaseLib.Abstracts;
using WoWTheSpire.WoWTheSpireCode.Extensions;
using Godot;

namespace WoWTheSpire.WoWTheSpireCode.Character;

public class WoWTheSpirePotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => WoWTheSpire.Color;


    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}