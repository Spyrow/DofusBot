using DofusBot.Protocol.Enums;

namespace DofusBot.Misc.Extensions
{
    public static class DirectionsEnumExtensions
    {
        public static DirectionsEnum GetOpposedDirection(this DirectionsEnum direction)
        {
            return (DirectionsEnum)((int)direction >= 4 ? (int)direction - 4 : (int)direction + 4);
        }
    }
}
