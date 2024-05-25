namespace SandsOfMaui
{
	public class HTMLBuilder
	{
		public string DOMToRender = string.Empty;

		public void BuildDOM(string IssueContent)
		{
			DOMToRender = "<!DOCTYPE html><html lang=\"en\">";
			DOMToRender += "<head><meta charset=\"utf-8\"/><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, user-scalable=yes\" />";
			DOMToRender += "<link href=\"https://fonts.googleapis.com/css?family=Open+Sans:400,600,300\" rel=\"stylesheet\" type=\"text/css\">";
			DOMToRender += "<style>" + LoadMauiAsset().Result + "</style>";
			DOMToRender += "</head><body>";
			DOMToRender += IssueContent;
			DOMToRender += "</body></html>";
		}

		async Task<string> LoadMauiAsset()
		{
			using var stream = await FileSystem.OpenAppPackageFileAsync("IssueStyle.css");
			using var reader = new StreamReader(stream);
			var contents = reader.ReadToEnd();
			return contents;
		}	
	}
}

