using Orchard.Data.Migration;

namespace Sunkist.OlarkLiveChat {
    public class Migrations : DataMigrationImpl {
        public int Create() {

            SchemaBuilder.CreateTable("LiveChatSettingsPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<bool>("Enable")
                    .Column<string>("Script", c => c.Unlimited())
                );

            return 1;
        }
    }
}