using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Sunkist.OlarkLiveChat.Models;

namespace Sunkist.OlarkLiveChat.Drivers {
    public class LiveChatSettingsPartDriver : ContentPartDriver<LiveChatSettingsPart> {
        private const string TemplateName = "Parts/LiveChatSettings";

        private readonly ISignals _signals;

        public LiveChatSettingsPartDriver(ISignals signals) {
            _signals = signals;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override string Prefix {
            get { return "LiveChatSettings"; }
        }

        protected override DriverResult Editor(LiveChatSettingsPart part, dynamic shapeHelper) {
            return ContentShape("Parts_LiveChatSettings_Edit",
                                () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix))
                .OnGroup("Live Chat");
        }

        protected override DriverResult Editor(LiveChatSettingsPart part, IUpdateModel updater, dynamic shapeHelper) {
            return ContentShape("Parts_LiveChatSettings_Edit", () => {
                updater.TryUpdateModel(part, Prefix, null, null);
                _signals.Trigger(LiveChatEvent.SettingsChanged);
                return shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix);
            })
                .OnGroup("Live Chat");
        }
    }
}