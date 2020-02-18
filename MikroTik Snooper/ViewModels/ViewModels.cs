using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikroTikSnooper
{
    public class ViewModels
    {
        public ConnectionViewModel ComboBoxWlan { get; } = new ConnectionViewModel();
        public SnooperListViewModel SnooperList { get; } = new SnooperListViewModel();

    }
}
