using System.Collections;
using Tools;
using UnityEngine;

namespace Assets._Project.Scripts.Storage.Static
{
    public class SystemData 
    {
        public static SystemData Instance;

        public SystemData()
        {
            if (Instance==null)
                Instance = this;
        }

        public int UID;

        public override string ToString() => this.GiveAllFields();
    }
}