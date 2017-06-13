using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFF.Freedom.SignalR
{
    public static class SignalRFeature
    {
        public static bool IsAvailable
        {
            get
            {
#if FEATURE_SIGNALR
                return true;
#else
                return false;
#endif
            }
        }
    }
}
