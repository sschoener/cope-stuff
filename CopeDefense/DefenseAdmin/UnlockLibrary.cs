using System;
using System.Collections.Generic;

namespace DefenseAdmin
{
    static class UnlockLibrary
    {
        private static readonly Dictionary<int, Unlock> s_unlocks = new Dictionary<int, Unlock>();

        internal static void InvokeUnlockAdded(Unlock unlock)
        {
            s_unlocks.Add(unlock.Id, unlock);
            Action<Unlock> handler = UnlockAdded;
            if (handler != null) handler(unlock);
        }

        internal static void InvokeUnlockRemoved(Unlock unlock)
        {
            s_unlocks.Remove(unlock.Id);
            Action<Unlock> handler = UnlockRemoved;
            if (handler != null) handler(unlock);
        }

        internal static event Action<Unlock> UnlockAdded;

        internal static event Action<Unlock> UnlockNameUpdated;

        internal static void InvokeUnlockNameUpdated(Unlock unlock)
        {
            Action<Unlock> handler = UnlockNameUpdated;
            if (handler != null) handler(unlock);
        }

        internal static event Action<Unlock> UnlockRemoved;

        internal static Dictionary<int, Unlock> CurrentUnlocks
        {
            get { return s_unlocks; }
        }

        internal static bool UnlocksFetched
        {
            get;
            private set;
        }

        internal static void GetUnlocks()
        {
            ServerInterface.GetAndParseUnlocks(s_unlocks);
            UnlocksFetched = true;
        }
    }
}