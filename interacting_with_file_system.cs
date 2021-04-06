using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace fileSystemPractice{

   // finding those files which are edited at the last weeeks 
 public class interactingwithfilesystem{
        public static void findLastWeekModifiedFile(string path)
        {

            Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                     .Select(filepath => new FileInfo(filepath))
                      .OrderByDescending(fileinfo => fileinfo.LastWriteTime)
                      .Where(fileinfo => fileinfo.LastWriteTime.AddDays(7).CompareTo(DateTime.Today) > 0)
                      .ToList()
                      .ForEach(fileinfo =>
                      {
                          Console.WriteLine(fileinfo.Name);

                      });
            Console.ReadLine();
        }

        // finding those file whose size is  0 bytees
        public static void LocatingDeadFile(string path)
        {

            Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                     .Select(filepath => new FileInfo(filepath))
                     .Where(FileInfo => FileInfo.Length == 0)
                     .ToList()
                     .ForEach(fileinfo =>
                     {

                         Console.WriteLine(fileinfo.Name);
                     });
        }

        // finding those file as depth search where whose content is same but those are  individual file with different name 
        // or same name in the different directories ...
        public static void findingexactDuplicatefile(string path)
        {

            var listofitems = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                      .Select(filepath => new
                      {
                          fileName = filepath,
                          contentHash = File.ReadAllText(filepath)
                                    .GetHashCode()
                      }).ToLookup(x => x.contentHash)
                         .Where(x => x.Count() >= 2)
                         .ToList();

            for (int x = 0; x < listofitems.Count(); x++) {

                foreach (var kvp in listofitems[x]) {
                    Console.WriteLine(kvp.fileName);



                }
            }


        }


        public static void findingTheToalSizeofDirectory(string path)
        {
            long totalSize = 0;

            Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                     .Select(filepath =>
                     {
                         return new FileInfo(filepath);
                     })
                     .ToList()
                     .ForEach(fileinfo =>
                     {

                         totalSize += fileinfo.Length;
                     });


            Console.WriteLine($"total size in the bytes"+totalSize);
            Console.WriteLine($"total size in the megabytes{totalSize / 1024 / 1000}");
            



        }


 }



}