using Microsoft.AspNetCore.Http;
using System.IO;

namespace SaveService.Common.FileConversion
{
    public static class FileHandler
    {
        public static byte[] ConvertFileToBiteArray(IFormFile fileToConvert)
        {
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(fileToConvert.OpenReadStream()))
            {
                fileData = binaryReader.ReadBytes((int)fileToConvert.Length);
            }
            return fileData;
        }
    }
}
