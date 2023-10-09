namespace StorageService
{
    public class StorageData
    {
        public int Level;
        public int AmountSoftResources;
        public int AmountHardResources;

        public override string ToString()
        {
            var str = "";
            foreach (var field in typeof(StorageData).GetFields())
                str += field.Name+ ": " + field .GetValue(this)+ "/";
            return str;
        }
    }
}