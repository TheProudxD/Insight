using Tools;

namespace StorageService
{
    public class DynamicData
    {
        public int CurrentLevel;
        public int AmountSoftResources;
        public int AmountHardResources;

        public override string ToString() => this.GiveAllFields();
    }
}