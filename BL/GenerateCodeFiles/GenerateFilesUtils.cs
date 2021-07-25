
using MongoDB.Bson;
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using WebApiCSharp.Services;
using WebApiCSharp.Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace WebApiCSharp.GenerateCodeFiles
{
    public class GenerateFilesUtils
    {
        public static void RunApplicationUntilEnd(string appFilePath, string workingDir = null, string arguments = null)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo()
            {
                FileName = appFilePath,
            };
            Process process = new Process();
            process.StartInfo = sInfo;
            if(workingDir != null)
            {
                process.StartInfo.WorkingDirectory = workingDir;
            }

            if(arguments != null)
            {
                process.StartInfo.Arguments = arguments;
            }


            process.StartInfo.UseShellExecute = true;
            process.Start();
            process.WaitForExit();
            process.Close();

        }
        public static string AppendPath(string basePath, string pathEnd)
        {
            basePath = basePath.EndsWith("/") ? basePath.TrimEnd('/') : basePath;
            pathEnd = pathEnd.StartsWith("/") ? pathEnd.TrimStart('/') : pathEnd;

            string resPath = basePath + "/" + pathEnd;
            return resPath;
        }
        public static string ToUpperFirstLetter(string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string GetIndentationStr(int numOfIndentations, int indentSize = 4, string str = "", bool withNewLine = true)
        {
            return (new string(' ', numOfIndentations * indentSize)) + str + Environment.NewLine;
        }


        public static void WriteTextFile(string path, string content)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(content);
            }
        }

        public static void DeleteAndCreateDirectory(string dirPath, bool createDirectory = true)
        {
            if (Directory.Exists(dirPath))
            {
                Directory.Delete(dirPath, true);
            }
            if (createDirectory)
            {
                Directory.CreateDirectory(dirPath);
            }
        }
    }
}