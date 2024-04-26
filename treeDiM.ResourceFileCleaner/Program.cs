#region Using directives
using System;
using System.Collections.Generic;
using System.Collections;
using System.Resources;
using System.IO;
#endregion

namespace treeDiM.ResourceFileCleaner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sDir = @"C:\\Users\\fgasn\\source\\repos\\StackBuilder\\Sources\\TreeDim.StackBuilder.Desktop";
            var extensions = new List<string>() { "fr", "es", "nl", "it", "ja","ru", "pl", "pt", "sl", "sv", "tr", "zh"};

            foreach (var extension in extensions) { 
            var files = Directory.GetFiles(sDir, $"*.{extension}.resx");
                foreach (var sFile in files)
                {
                    Console.WriteLine($"Processing {Path.GetFileName(sFile)}...");
                    try
                    {
                        if (!string.Equals(Path.GetExtension(sFile), ".resx", StringComparison.CurrentCultureIgnoreCase)) continue;
                        ResXResourceReader rr = new ResXResourceReader(sFile);
                        ResXResourceWriter rw = new ResXResourceWriter($"{sFile}_New");
                        foreach (DictionaryEntry de in rr)
                        {
                            if (de.Key.ToString().EndsWith("Text"))
                            {
                                rw.AddResource(de.Key.ToString(), de.Value.ToString());
                            }
                        }
                        rr.Close();
                        rw.Close();
                        
                        File.Delete(sFile);
                        File.Copy($"{sFile}_New", sFile);
                        File.Delete($"{sFile}_New");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{Path.GetFileName(sFile)}: {ex.Message}");
                    }
                }                
            }
        }
    }
}
