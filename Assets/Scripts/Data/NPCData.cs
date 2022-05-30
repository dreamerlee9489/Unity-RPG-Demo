namespace App.Data
{
    [System.Serializable]
	public class NPCData
	{
        public int index = 0;
		public float currentHP = 0;
        public float currentMP = 0;
        public Vector position;
        public NPCData() {}
	}
}