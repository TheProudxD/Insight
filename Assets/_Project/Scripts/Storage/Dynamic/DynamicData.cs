using Tools;

namespace StorageService
{
    public class DynamicData
    {
        public int CurrentLevel;
        public int AmountSoftResources;
        public int AmountHardResources;
        public string Name;

        public override string ToString() => this.GiveAllFields();
    }
}