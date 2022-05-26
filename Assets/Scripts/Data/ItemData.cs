using App.Items;

namespace App.Data
{
    [System.Serializable]
    public class ItemData
    {
        public string name = "";
        public string path = "";
        public int level = 0;
        public ContainerType containerType = ContainerType.WORLD;
        public ItemData() {}
        public ItemData(string name, string path, int level, ContainerType containerType)
        {
            this.name = name;
            this.path = "Items/" + path;
            this.level = level;
            this.containerType = containerType;
        }
    }
}
