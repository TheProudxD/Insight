namespace StorageService
{
    public class DynamicData
    {
        public int CurrentLevel;
        public int AmountSoftResources;
        public int AmountHardResources;

        public override string ToString()
        {
            var str = "";
            foreach (var field in GetType().GetFields())
                str += field.Name+ ": " + field .GetValue(this)+ "/";
            return str;
        }
    }
}