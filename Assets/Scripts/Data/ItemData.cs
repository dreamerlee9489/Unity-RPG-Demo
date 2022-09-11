using Items;

namespace Data
{
    [System.Serializable]
    public class ItemData
    {
        public string id = "";
        public string path = "";
        public int level = 0;
        public Vector position;
        public ContainerType containerType = ContainerType.World;

        public ItemData()
        {
        }

        public override bool Equals(object obj)
        {
            ItemData itemData = obj as ItemData;
            return id.Equals(itemData.id);
        }

        public override int GetHashCode()
        {
            return path.GetHashCode() ^ position.GetHashCode();
        }

        public override string ToString()
        {
            return path + position.ToString();
        }
    }
}