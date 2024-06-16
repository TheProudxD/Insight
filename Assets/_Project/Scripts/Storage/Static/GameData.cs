using Tools;

namespace StorageService
{
    public class GameData
    {
        public int MaxLevel;
        public int MaxEnergy;
        
        public override string ToString() => this.GiveAllFields();
    }
}