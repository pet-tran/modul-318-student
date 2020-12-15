using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FBB.Library;

namespace FBB.Model
{
    public class TransportViewModel : NotifyPropertyChanged
    {

        TransportViewModel()
        {
            object command = new RelayCommand<object>(ExecuteMethod, CanExecuteMethod);
            this.Command = (RelayCommand<object>)command;
        }

        public RelayCommand<object> Command;

        public void ExecuteMethod(object obj)
        {

        }

        public bool CanExecuteMethod(object obj)
        {
            return true;
        }
    }
}
