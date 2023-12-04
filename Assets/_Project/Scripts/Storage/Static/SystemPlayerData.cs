using Tools;

namespace Assets._Project.Scripts.Storage.Static
{
    public class SystemPlayerData 
    {
        public static SystemPlayerData Instance;

        public SystemPlayerData() => Instance ??= this;

        public int UID;
        public string Name;
        public long Key;

        public override string ToString() => this.GiveAllFields();
    }
}