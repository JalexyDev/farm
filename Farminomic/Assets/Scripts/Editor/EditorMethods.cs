using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;

public class EditorMethods : Editor
{
    const string extension = ".cs";
    public static void WriteProductNamesEnum(string path, string name, List<Product> products)
    {
        using (StreamWriter file = File.CreateText(path + name + extension))
        {
            file.WriteLine("public enum " + name + " \n{");

            for (int i = 0; i < products.Count; i++)
            {
                string lineRep = products[i].EnumName.Replace(" ", string.Empty);

                if (!string.IsNullOrEmpty(lineRep) && !Regex.IsMatch(lineRep, @"\d"))
                {
                    if (i != products.Count - 1)
                    {
                        file.WriteLine(string.Format("\t{0} = {1},", lineRep, i));
                    }
                    else
                    {
                        file.WriteLine(string.Format("\t{0} = {1}", lineRep, i));
                    }
                }
            }

            file.WriteLine("\n}");
        }

        AssetDatabase.ImportAsset(path + name + extension);
    }
}
