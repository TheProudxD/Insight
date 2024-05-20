using Tools;

namespace StorageService
{
    public class PlayerData
    {
        public int CurrentLevel;
        public int AmountSoftResources;
        public int AmountHardResources;
        public int AmountEnergy;
        public string Name;

        public override string ToString() => this.GiveAllFields();
    }
}