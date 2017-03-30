GitHub Monitor
==============
[![Build status](https://ci.appveyor.com/api/projects/status/lf6e10mr2q92tj4w?svg=true)](https://ci.appveyor.com/project/ampersandre/github-monitor)

The goal is to build a web UI to display PRs, how long they have been waiting for a review, whether there are any failing hooks, and bring to attention any stale branches


Requirements
------------

1. Get an [Access Token](https://github.com/settings/tokens) from GitHub with the `repo:status` scope
2. Either set an environment variable called `GITHUB_ACCESS_TOKEN` or set the `gitHubAccessToken` App Setting in Web.Config
3. Run the Solution!
