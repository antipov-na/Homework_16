using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_16_1
{
    interface ICloseRequest
    {
        event EventHandler<CloseRequestArgs> CloseRequest;
    }
    class CloseRequestArgs : EventArgs
    {
        public bool? DialogResult { get; set; }
        public CloseRequestArgs(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }
    }
}
