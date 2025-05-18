namespace cope.Relic.RelicBinary
{
    public interface IRBFKeyProvider
    {
        /// <summary>
        /// Returns the key at the given index. May throw exceptions.
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        string GetNameByIndex(int idx);

        /// <summary>
        /// Returns the index for the key or -1 if the key could not be found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int GetIndexForName(string name);

        /// <summary>
        /// Tries to get the key at the given index. Returns true on success.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        bool TryGetName(int index, out string name);

        /// <summary>
        /// Adds a key and returns the index.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int AddKey(string key);

        /// <summary>
        /// Returns whether this IRBFKeyProvider needs to be updated.
        /// </summary>
        /// <returns></returns>
        bool NeedsUpdate();

        /// <summary>
        /// Called when the new keys have been added to the IRBFKeyProvider.
        /// </summary>
        void Update();
    }
}