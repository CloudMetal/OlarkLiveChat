using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;

namespace Sunkist.OlarkLiveChat.Models {
    public class LiveChatSettingsPart : ContentPart<LiveChatSettingsPartRecord> {
        public bool Enable {
            get { return Record.Enable; }
            set { Record.Enable = value; }
        }

        public string Script {
            get { return Record.Script; }
            set { Record.Script = value; }
        }

        public bool IsValid() {
            return !Record.Enable || !string.IsNullOrWhiteSpace(Record.Script);
        }
    }

    public class LiveChatSettingsPartRecord : ContentPartRecord {
        public virtual bool Enable { get; set; }
        public virtual string Script { get; set; }
    }
}