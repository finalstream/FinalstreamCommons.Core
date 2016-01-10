using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FinalstreamCommons.Extensions
{
    public static class RectExtensions
    {
        public static bool IsAllZero(this Rect rect)
        {
            return rect.Top == 0.0 && rect.Left == 0.0 && rect.X == 0.0 && rect.Y == 0.0;
        }
    }
}
