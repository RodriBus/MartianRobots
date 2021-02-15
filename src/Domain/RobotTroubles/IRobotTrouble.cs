namespace RodriBus.MartianRobots.Domain.RobotTroubles
{
    /// <summary>
    /// Represents a robot trouble contract.
    /// </summary>
    public interface IRobotTrouble
    {
        /// <summary>
        /// Gets the trouble code.
        /// </summary>
        public string GetTroubleCode();
    }
}