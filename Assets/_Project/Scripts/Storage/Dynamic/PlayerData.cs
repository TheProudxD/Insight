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

        public override string ToString() => this.GiveAllFields();
    }
}