using System.Windows.Media;

namespace FinalstreamCommons.Utils
{
    /// <summary>
    ///     カラーユーティリティを表します。
    /// </summary>
    public static class ColorUtils
    {
        /// <summary>
        ///     反対色を取得します。
        /// </summary>
        /// <param name="baseColor"></param>
        /// <returns></returns>
        public static Color GetReverseColor(Color baseColor)
        {
            var sum = baseColor.R + baseColor.G + baseColor.B;

            if (((255*3)/2) > sum)
            {
                // DarkだからLightを
                return GetLighterColor(baseColor, 125);
            }
            // LightだからDarkを
            return GetDarkerColor(baseColor, 125);
        }

        /// <summary>
        ///     反対色を取得します。
        /// </summary>
        /// <param name="baseColor"></param>
        /// <returns></returns>
        public static Color GetReverseColor(Color baseColor, int gradientPower)
        {
            var sum = baseColor.R + baseColor.G + baseColor.B;

            if (((255*3)/2) > sum)
            {
                // DarkだからLightを
                return GetLighterColor(baseColor, gradientPower);
            }
            // LightだからDarkを
            return GetDarkerColor(baseColor, gradientPower);
        }

        /// <summary>
        ///     Returns a color which is lighter than the given color.
        /// </summary>
        /// <param name="color">Color</param>
        /// <param name="gradientPower"></param>
        /// <returns>lighter color</returns>
        public static Color GetLighterColor(Color color, int gradientPower)
        {
            return Color.FromArgb(255, AddValueMax255(color.R, gradientPower), AddValueMax255(color.G, gradientPower),
                AddValueMax255(color.B, gradientPower));
        }

        /// <summary>
        ///     Add two values but do not return a value greater than 255.
        /// </summary>
        /// <param name="input">first value</param>
        /// <param name="add">value to add</param>
        /// <returns>sum of both values</returns>
        private static byte AddValueMax255(int input, int add)
        {
            return (byte) ((input + add < 256) ? input + add : 255);
        }

        /// <summary>
        ///     Subtract two values but do not returns a value below 0.
        /// </summary>
        /// <param name="input">first value</param>
        /// <param name="ded">value to subtract</param>
        /// <returns>first value minus second value</returns>
        private static byte DedValueMin0(int input, int ded)
        {
            return (byte) ((input - ded > 0) ? input - ded : 0);
        }

        /// <summary>
        ///     Returns a color which is darker than the given color.
        /// </summary>
        /// <param name="color">Color</param>
        /// <param name="gradientPower"></param>
        /// <returns>darker color</returns>
        public static Color GetDarkerColor(Color color, int gradientPower)
        {
            return Color.FromArgb(255, DedValueMin0(color.R, gradientPower), DedValueMin0(color.G, gradientPower),
                DedValueMin0(color.B, gradientPower));
        }
    }
}