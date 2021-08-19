using DevExpress.DashboardWin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevExpresss.DashboardWin.CustomItemExtension {
    public interface IExtensionModule {
        void AttachViewer(DashboardViewer viewer);
        void DetachViewer();
        void AttachDesigner(DashboardDesigner designer);
        void DetachDesigner();
    }
}
