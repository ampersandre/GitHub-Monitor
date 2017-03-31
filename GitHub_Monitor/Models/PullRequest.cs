using System;
using System.Web.UI.WebControls;

namespace GitHub_Monitor.Models
{
	public class PullRequest : GitHubEntity
	{
		public int Number { get; set; }
		public PullRequestState State { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
		public User Assignee { get; set; }
		public DateTime CreateDate { get; set; }
	}

	public enum PullRequestState
	{
		Open, Closed
	}
}