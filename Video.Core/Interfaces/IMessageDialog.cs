using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Video.Core.Interfaces
{
    public interface IMessageDialog
    {
        void SendMessage(string message, string title = null);
    }
}
