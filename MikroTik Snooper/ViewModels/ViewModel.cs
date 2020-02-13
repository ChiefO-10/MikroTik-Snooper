using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikroTikSnooper
{
    public class ViewModel
    {
        public ComboBoxViewModel ComboBox { get; } = new ComboBoxViewModel();
        public SnooperListViewModel SnooperList { get; } = new SnooperListViewModel();

    }
}
