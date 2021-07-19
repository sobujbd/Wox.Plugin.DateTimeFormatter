using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;
using System.Diagnostics;
using System.IO;

namespace Wox.Plugin.DateTimeFormatter
{
    public class Main : IPlugin
    {
        private PluginInitContext _context;
        private readonly string pluginPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public List<Result> Query(Query query)
        {
            List<Result> results = new List<Result>();

            //get the input entered by the user
            string input = query.Search;
  
            String title, subTitle;
            TextInfo convertCase = new CultureInfo("en-US", false).TextInfo;
            DateTimeOffset DatetimeNow = DateTimeOffset.Now;

            title = DatetimeNow.ToString(input, CultureInfo.InvariantCulture);
            //subTitle = convertCase.ToUpper(input);
            subTitle = input;

            // Format date
            results.Add(ResultForTextCopy(title, subTitle, title));
            // Details about custom date time format
            results.Add(ResultForOpenSiteInBrowser("Details about standard date and time format strings", "https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#table-of-format-specifiers"));

            return results;
        }

        public void Init(PluginInitContext context)
        {
            _context = context;
        }

        private Result ResultForTextCopy(String title, String subtitle, String text)
        {
            if (subtitle != null)
            {
                var result = new Result
                {
                    Title = title,
                    SubTitle = subtitle,
                    IcoPath = "Images\\icon.png",
                    Score = 99999,
                    Action = (x) =>
                    {
                        try
                        {
                            Clipboard.SetText(text.TrimStart());
                            _context.API.ShowMsg("Copied!", text.TrimStart(), $"{pluginPath}\\Images\\icon.png");
                            return true;
                        }
                        catch
                        {
                            var message = "Fail to set text in clipboard!";
                            _context.API.ShowMsg(message, string.Empty, $"{pluginPath}\\Images\\icon.png");
                            return false;
                        }
                    }
                };

                return result;
            }

            return new Result { Title = "No items..." };
        }

        private Result ResultForOpenSiteInBrowser(String title, String url)
        {
            if (url != null)
            {
                var result = new Result
                {
                    Title = title,
                    SubTitle = url,
                    IcoPath = "Images\\open_in_browser.png",
                    Score = 1,
                    Action = (x) =>
                    {
                        try
                        {
                            Process.Start("https://rebrand.ly/t57dtcr");
                            return true;
                        }
                        catch
                        {
                            _context.API.ShowMsg($"Can not open url {url}", "", "Images\\icon.png");
                            return false;
                        }
                    }
                };

                return result;
            }

            return new Result { Title = "No items..." };
        }

    }
}
