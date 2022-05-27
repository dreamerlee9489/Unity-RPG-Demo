namespace App.Data
{
    [System.Serializable]
	public class EntityData
	{
		public string path = "";
        public EntityData() {}
        public EntityData(string path)
        {
            this.path = "Entity/" + path;
        }
	}
}
