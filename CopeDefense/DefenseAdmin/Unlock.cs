using DefenseShared;

namespace DefenseAdmin
{
    internal class Unlock
    {
        public int Id;
        public int RequiredUnlockId;
        public int UnlockGroup;
        public int Price;

        public string Name
        {
            get { return ItemDatabases.Unlocks.GetName(Id); }
        }

        public string Description
        {
            get { return ItemDatabases.Unlocks.GetDesc(Id); }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}