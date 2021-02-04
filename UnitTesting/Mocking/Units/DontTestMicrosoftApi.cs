using System.IO;
using System.Threading.Tasks;

namespace Mocking.Units
{
    public class DontTestMicrosoftApi
    {
        public static Task SaveFile(string path, Stream file)
        {
            var fileStream = File.OpenWrite("path");
            return file.CopyToAsync(fileStream);
        }
    }
}