using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

 public class Program
 {

     public static string className;

     static void Main(string[] args)
     {
         
         String filePath = "test.txt";
         String newFilePath = "test2.txt";
         
         //call static methods
         ReadWriteFiles(filePath, newFilePath);

     }

     public string FirstLetterToLower(string str)
     {
         if (str == null)
             return null;

         if (str.Length > 1)
             return char.ToLower(str[0]) + str.Substring(1);

         return str.ToLower();
     }

     public string MUnderscore(string str)
     {
         if (str == null)
             return null;

         return "m_" + FirstLetterToLower(str);
     }

     public string textNodeCode(string str)
     {
         if (str == null)
             return null;

         return MUnderscore(str) + " = GetTextNode(\"" + className + "/insertXMLPathHere/" + str + "\", element)";
     }

     public string controlScriptVar(string str)
     {
         if (str == null)
             return null;

         return MUnderscore(str) + "Text";
     }

     public string controlScriptCode(string str)
     {
         if (str == null)
             return null;

         return controlScriptVar(str) + " = GetControl( \"input_" + FirstLetterToLower(str) + "\" )";
     }

     public string setTextCode(string str)
     {

         if (str == null)
             return null;

         return MUnderscore(str) + "Text.Text = MedicalRecord." + className + "." + MUnderscore(str);
     }

     public static void ReadWriteFiles(string filePath, string newFilePath)
     {
         List<string> lines = File.ReadAllLines(filePath).ToList();
         
         Program writer = new Program();

         int initCount = lines.Count;

         className = lines[0];

         // Code Formatting

         lines.Add("/////");
         lines.Add("C# public variables");
         lines.Add("/////");

         for (int i = 1; i < initCount; i++)
         {
             lines.Add(("public string " + writer.MUnderscore(lines[i]) + ";"));
         }

         lines.Add("/////");
         lines.Add("C# get text node code");
         lines.Add("/////");

         for (int i = 1; i < initCount; i++)
         {
             lines.Add((writer.textNodeCode(lines[i]) + ";"));
         }

         lines.Add("/////");
         lines.Add("asset builder Text variable list for copy paste into guicontrolscript section");
         lines.Add("/////");

         for (int i = 1; i < initCount; i++)
         {
             lines.Add((writer.controlScriptVar(lines[i])));
         }

         lines.Add("/////");
         lines.Add("asset builder onInit get control variables");
         lines.Add("/////");

         for (int i = 1; i < initCount; i++)
         {
             lines.Add((writer.controlScriptCode(lines[i]) + ";"));
         }

         lines.Add("/////");
         lines.Add("asset builder onInit set textbox equal to var");
         lines.Add("/////");

         for (int i = 1; i < initCount; i++)
         {
             lines.Add((writer.setTextCode(lines[i]) + ";"));
         }

         File.WriteAllLines(newFilePath, lines);
     }
 }

