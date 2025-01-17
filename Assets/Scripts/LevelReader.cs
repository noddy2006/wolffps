﻿using UnityEngine;
using System.Collections;
using System.IO;

public class LevelReader {

    public static Level LoadLevel(string levelFileName, Material[] allMaterials)
    {
        StreamReader stream = new StreamReader(levelFileName);
        
        // First line is the level dimensions (w,h)
        string levelSize = stream.ReadLine();
        string[] levelDimens = levelSize.Split(',');
        int width = int.Parse(levelDimens[0]);
        int height = int.Parse(levelDimens[1]);

        // Second line is the number (n) of different wall materials; following n lines are material names.
        int wallMaterialCount = int.Parse(stream.ReadLine());
        Material[] levelMaterials = new Material[wallMaterialCount];
        for (int m = 0; m < wallMaterialCount; m++)
        {
            string mName = stream.ReadLine();
            foreach(Material material in allMaterials) {
                if (material.name == mName)
                {
                    levelMaterials[m] = material;
                    break;
                }
            }
        }

        // Remaining h lines is the level definition
        char[,] level = new char[width,height];
        for (int y = 0; y < height; y++)
        {
            string rowStr = stream.ReadLine();
            char[] row = rowStr.ToCharArray();
            for (int x = 0; x < width; x++)
            {
                if (x < row.Length && IsValidSpace(row[x])) 
                {
                    level[x, y] = row[x];
                }
            }
        }

        stream.Close();

        return new Level(levelMaterials, level);
    }

    private static bool IsValidSpace(char space)
    {
        return char.IsLetterOrDigit(space);
    }

}
