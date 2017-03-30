namespace GitHub_Monitor.Models
{
	public abstract class GitHubEntity
	{
		public int Id { get; set; }
		public string HtmlUrl { get; set; }
	}
}