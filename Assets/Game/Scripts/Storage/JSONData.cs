namespace StorageService
{
    public class JSONData
    {
        public int MaxLevel;

        public override string ToString()
        {
            var str = "";
            foreach (var field in typeof(JSONData).GetFields())
                str += field.Name+ ": " + field .GetValue(this)+ "/";
            return str;
        }
    }
}