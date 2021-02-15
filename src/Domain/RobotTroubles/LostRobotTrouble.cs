namespace RodriBus.MartianRobots.Domain.RobotTroubles
{
    /// <summary>
    /// Represents a lost robot trouble.
    /// </summary>
    public struct LostRobotTrouble : IRobotTrouble
    {
        private const string TroubleCode = "LOST";

        /// <summary>
        /// Gets the trouble code.
        /// </summary>
        public string GetTroubleCode() => TroubleCode;
    }
}