namespace KleinGarterRevision
{
    public enum State
    {
        /// <summary>
        /// Available for loans
        /// </summary>
        Free,
        /// <summary>
        /// IUnit is being loaned by a user
        /// </summary>
        Lended,
        /// <summary>
        /// IUnit is reserved for maintenance
        /// </summary>
        Reserved
    }
}