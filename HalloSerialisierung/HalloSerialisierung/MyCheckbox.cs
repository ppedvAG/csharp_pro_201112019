using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalloSerialisierung
{
    public class MyCheckbox : CheckBox
    {
        [Browsable(true)]
        public event Action<object, int> MyEvent;

    }
}
