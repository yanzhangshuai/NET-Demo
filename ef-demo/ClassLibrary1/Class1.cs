using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace ClassLibrary1
{
    public class Class1
    {
        public void Show()
        {
            foreach (var entry in GetAllAssembly())
            {
             
                Console.WriteLine(entry);
            }
          
            // var data = DependencyContext.Default.CompileLibraries.Where(y =>
            //     !y.Name.StartsWith("System.") && !y.Name.StartsWith("Microsoft.")).ToArray();
            // foreach (var library in data)
            // {
            //     Console.WriteLine(library.Name);
            //
            // }
        }
        public  IList<string> GetAllAssembly()
        {
            return Directory.GetFileSystemEntries(Directory.GetCurrentDirectory())
                .Select(Path.GetFileName)
                .Where(file => file.EndsWith(".dll") && !file.StartsWith("System.") && !file.StartsWith("Microsoft."))
                .ToList();
        }
    }
}