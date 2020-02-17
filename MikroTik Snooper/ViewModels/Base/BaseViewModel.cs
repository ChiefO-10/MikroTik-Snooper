using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MikroTikSnooper
{
    /// <summary>
    /// Base view model - Propety Changed event firing
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///  Event firing when any child property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Method invoking PropertyChanged event
        /// CallerMemberName allows to use OnPropertyChanged()
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
