using Tools;

namespace Assets._Project.Scripts.Storage.Static
{
    public class SystemPlayerData 
    {
        public static SystemPlayerData Instance;

        public SystemPlayerData(int uID, string key)
        {
            uid = uID;
            this.key = key;
            Instance ??= this;
        }

        public int uid;
        public string key;

        public override string ToString() => this.GiveAllFields();

        public override bool Equals(object obj)
        {
            var newData = obj as SystemPlayerData;
            if (newData == null)
                return false;

            return newData.uid == uid;
        }

        public override int GetHashCode()
        {
            return uid.GetHashCode()*13;
        }
    }
}