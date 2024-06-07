using SimpleJSON;
using Tools;

namespace Storage.Static
{
    public class SystemPlayerData
    {
        public static SystemPlayerData Instance;

        public readonly int uid;
        public readonly string key;

        public SystemPlayerData(int uid, string key)
        {
            this.uid = uid;
            this.key = key;
        }

        public void ToSingleton() => Instance = this;
        public override string ToString() => this.GiveAllFields();

        public override bool Equals(object obj)
        {
            if (obj is not SystemPlayerData newData)
                return false;

            return newData.uid == uid;
        }

        public static SystemPlayerData Parse(JSONNode data)
        {
            var uid = int.Parse(data["uid"]);
            var key = data["key"].Value;
            var systemData = new SystemPlayerData(uid, key);
            return systemData;
        }

        public override int GetHashCode() => uid.GetHashCode() * 19 + key.GetHashCode() * 13;
    }
}