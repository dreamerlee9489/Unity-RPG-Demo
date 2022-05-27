using App.Items;

namespace App.Data
{
    [System.Serializable]
    public class ItemData
    {
        public string path = "";
        public int level = 0;
        public ContainerType containerType = ContainerType.WORLD;
        public ItemData() {}
        public ItemData(string path, int level, ContainerType containerType)
        {
            this.path = "Items/" + path;
            this.level = level;
            this.containerType = containerType;
        }
    }
}
