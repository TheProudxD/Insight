using Tools;

namespace StorageService
{
    public class StaticData
    {
        public int MaxLevel;

        public override string ToString() => this.GiveAllFields();
        public override int GetHashCode() => MaxLevel * 13;
    }
}