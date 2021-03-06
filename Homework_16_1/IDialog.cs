using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Homework_16_1
{
    interface IDialog
    {
        object DataContext { get; set; }
        void Close();
        bool? ShowDialog();
        Window Owner { get; set; }
        bool? DialogResult { get; set; }
    }
}
