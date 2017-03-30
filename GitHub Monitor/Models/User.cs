namespace GitHub_Monitor.Models
{
	public class User : GitHubEntity
	{
		public string Login { get; set; }
		public string AvatarUrl { get; set; }
	}
}