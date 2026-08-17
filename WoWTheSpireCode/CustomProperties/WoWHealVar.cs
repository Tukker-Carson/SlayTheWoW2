using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace WoWTheSpire.WoWTheSpireCode.CustomProperties;

public class WoWHealVar : DynamicVar {
      public const string defaultName = "WoWHeal";

      public ValueProp Props { get; set; }

      public WoWHealVar(Decimal heal, ValueProp props) : base(defaultName, heal) { 
          Props = props;
      }

      public WoWHealVar(string name, Decimal damage, ValueProp props) : base(name, damage) {
          Props = props;
      }

      public override void UpdateCardPreview(CardModel card, CardPreviewMode previewMode, Creature? target, bool runGlobalHooks) {
          // Doesn't take enchantments into consideration
          if (runGlobalHooks) 
              PreviewValue = WoWCmd.ResolveHealAmount(target ?? card.Owner.Creature, card.Owner.Creature, BaseValue, Props, null);
      }
}
