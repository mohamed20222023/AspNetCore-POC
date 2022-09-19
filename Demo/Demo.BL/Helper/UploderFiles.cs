using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Helper
{
    public static class UploderFiles
    {

        public static string GetFileName(IFormFile fileUrl , string FolderName)
        {
            //1 ) Get Directory
             string FolderPath = Path.Combine(Directory.GetCurrentDirectory() ,"wwwroot/Files" ,FolderName);


            //2) Get File Name
              string FileName = Guid.NewGuid() + Path.GetFileName(fileUrl.FileName);


            //3) Merge Path with File Name
             string FinalPath = Path.Combine(FolderPath, FileName);


            //4) Save File As Streams "Data Overtime"
              using (var Stream = new FileStream(FinalPath, FileMode.Create))

            {

                fileUrl.CopyTo(Stream);

            }

            return FileName;
        }

        public static string RemoveFile( string FolderName , string FileName)
        {
            try
            {
                if (File.Exists(Directory.GetCurrentDirectory() + "/wwroot/Files/" +FolderName +"/" + FileName))
                {
                    File.Delete(Directory.GetCurrentDirectory() +"/"+FolderName +"/"+ FileName);
                }
                return "File Deleted";
            }
            catch(Exception ex)
            {
                return "File Deleted";
            }
        }

    }
}
