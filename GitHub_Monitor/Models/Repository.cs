namespace GitHub_Monitor.Models
{
	public class Repository : GitHubEntity
	{
		public string Name { get; set; }
		public Owner Owner { get; set; }
		public int ForksCount { get; set; }
		public int StarGazersCount { get; set; }
		public int WatchersCount { get; set; }
		public int OpenIssuesCount { get; set; }
	}
}