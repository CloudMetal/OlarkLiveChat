using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Localization;
using Sunkist.OlarkLiveChat.Models;

namespace Sunkist.OlarkLiveChat.Handlers {
    public class LiveChatSettingsPartHandler : ContentHandler {
        public LiveChatSettingsPartHandler(IRepository<LiveChatSettingsPartRecord> repository) {
            T = NullLocalizer.Instance;

            Filters.Add(new ActivatingFilter<LiveChatSettingsPart>("Site"));
            Filters.Add(StorageFilter.For(repository));
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Live Chat")));
        }
    }
}