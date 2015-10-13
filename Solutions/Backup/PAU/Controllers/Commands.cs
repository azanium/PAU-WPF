using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevComponents.WpfRibbon;

namespace PAU.Controllers
{
    public class Commands
    {
        public static ButtonDropDownCommand NewLevel = new ButtonDropDownCommand("New Level", "NewLevelCommand", typeof(Ribbon));
        public static ButtonDropDownCommand OpenLevel = new ButtonDropDownCommand("Open Level", "OpenLevelCommand", typeof(Ribbon));
        public static ButtonDropDownCommand SaveLevel = new ButtonDropDownCommand("Save Level", "SaveLevelCommand", typeof(Ribbon));
        public static ButtonDropDownCommand SaveLevelAs = new ButtonDropDownCommand("Save Level As", "SaveLevelAsCommand", typeof(Ribbon));
        public static ButtonDropDownCommand CloseLevel = new ButtonDropDownCommand("Close Level", "CloseLevelCommand", typeof(Ribbon));

        public static ButtonDropDownCommand ImportManifest = new ButtonDropDownCommand("Import AirAsia Manifest", "importManifest", typeof(Ribbon));
        public static ButtonDropDownCommand ImportManifest2 = new ButtonDropDownCommand("Import AirAsia Manifest 2", "importManifest2", typeof(Ribbon));

        public static ButtonDropDownCommand RefreshData = new ButtonDropDownCommand("Refresh Database", "refreshDatabase", typeof(Ribbon));
        public static ButtonDropDownCommand AddDPOData = new ButtonDropDownCommand("Add DPO Data", "addDPOData", typeof(Ribbon));
        public static ButtonDropDownCommand ConnectionSettings = new ButtonDropDownCommand("Connection Settings", "connectionSettings", typeof(Ribbon));
        public static ButtonDropDownCommand AddWNAttention = new ButtonDropDownCommand("Add WN Attention", "addWNAttention", typeof(Ribbon));

        public static ButtonDropDownCommand BackPage = new ButtonDropDownCommand("Back", "backPage", typeof(Ribbon));
        public static ButtonDropDownCommand NextPage = new ButtonDropDownCommand("Next", "nextPage", typeof(Ribbon));
    }
}
