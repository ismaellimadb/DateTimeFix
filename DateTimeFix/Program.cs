using System.Net;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;


WebClient client = new WebClient();
string url = "http://worldtimeapi.org/api/ip";
Byte[] requestedHTML;
requestedHTML = client.DownloadData(url);

UTF8Encoding objUTF8 = new UTF8Encoding();
string html = objUTF8.GetString(requestedHTML);

string datePattern = @"(\d{4})-(\d{2})-(\d{2})"; 
string timePattern = @"(\d{2}):(\d{2}):(\d{2})"; 

string date = Regex.Match(html, datePattern).Value;
string time = Regex.Match(html, timePattern).Value;

var dateParts = date.Split('-');
string year = dateParts[0];
string month = dateParts[1];
string day = dateParts[2];

string formattedDate = $"{day}/{month}/{year}";

string strCmdFixDate = $"date {formattedDate}";
string strCmdFixTime = $"time {time}";

Process cmd = new Process();
cmd.StartInfo.FileName = "cmd.exe";
cmd.StartInfo.RedirectStandardInput = true;
cmd.StartInfo.RedirectStandardOutput = true;
cmd.StartInfo.CreateNoWindow = true;
cmd.StartInfo.UseShellExecute = false;
cmd.Start();

cmd.StandardInput.WriteLine(strCmdFixTime);
cmd.StandardInput.WriteLine(strCmdFixDate);
cmd.StandardInput.Flush();
cmd.StandardInput.Close();
cmd.WaitForExit();
Console.WriteLine(cmd.StandardOutput.ReadToEnd());