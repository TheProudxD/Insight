using Tools;

namespace StorageService
{
    public class PlayerData
    {
        public int MaxPassedLevel;
        public int AmountSoftResources;
        public int AmountHardResources;
        public int AmountEnergy;
        public string Name;

        public long DifferenceLastPlay; // in minutes
        
        public override string ToString() => this.GiveAllFields();
    }
}