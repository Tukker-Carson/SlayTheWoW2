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
      public Creature? Target { get; set; }

      
      public WoWHealVar(Decimal heal, ValueProp props) : base(defaultName, heal) { 
          Props = props;
          Target = null;
      }

      public WoWHealVar(string name, Decimal heal, ValueProp props) : base(name, heal) {
          Props = props;
          Target = null;
      }

      public WoWHealVar(Decimal heal, ValueProp props, Creature? target) : base(defaultName, heal) {
          Props = props;
          Target = target;
      }

      public WoWHealVar(string name, Decimal heal, ValueProp props, Creature? target) : base(name, heal) {
          Props = props;
          Target = target;
      }
      

      public override void UpdateCardPreview(CardModel card, CardPreviewMode previewMode, Creature? target, bool runGlobalHooks) {
          // Doesn't take enchantments into consideration
          if (runGlobalHooks) 
              PreviewValue = WoWCmd.ResolveHealAmount(Target ?? target ?? card.Owner.Creature, card.Owner.Creature, BaseValue, Props, null);
      }
}
