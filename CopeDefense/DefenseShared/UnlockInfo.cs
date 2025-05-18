namespace DefenseShared
{
    /// <summary>
    /// Information about unlockable stuff.
    /// </summary>
    public class UnlockInfo
    {
        /// <summary>
        /// Price of this unlock.
        /// </summary>
        public int Price;

        /// <summary>
        /// The id of this unlock.
        /// </summary>
        public int Id;

        /// <summary>
        /// The id of the required unlock for this unlock or -1 if there is no requirement.
        /// </summary>
        public int RequiredId;

        public override string ToString()
        {
            return ItemDatabases.Unlocks.GetName(Id);
        }
    }
}