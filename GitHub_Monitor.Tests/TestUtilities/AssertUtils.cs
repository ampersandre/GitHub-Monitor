using Newtonsoft.Json;
using NUnit.Framework;

namespace GitHub_Monitor.Tests.TestUtilities
{
	public class AssertUtils
	{
		public static void AreIdentical(object expected, object actual)
		{
			var expectedJson = JsonConvert.SerializeObject(expected);
			var actualJson = JsonConvert.SerializeObject(actual);

			if (!string.Equals(expectedJson, actualJson))
			{
				var assertionMessage = $@"Objects were not identical.
====== Expected ======
${expectedJson}

======= Actual =======
${actualJson}";

				throw new AssertionException(assertionMessage);
			}
		}
	}
}
