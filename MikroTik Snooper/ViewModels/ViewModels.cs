using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikroTikSnooper
{
    public class ViewModels
    {
        public ComboBoxViewModel ComboBoxWlan { get; } = new ComboBoxViewModel();
        public SnooperListViewModel SnooperList { get; } = new SnooperListViewModel();

    }
}
