﻿#if UNITY_IOS || UNITY_TVOS
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.CoreLibrary.Editor.NativePlugins.Build.Xcode
{
    public static class PreprocessorDirectives
    {
        #region Constants

        private     static      string      s_configFilePath    = NativePluginsPackageLayout.IosPluginPath + "/NPConfig.h";

        #endregion

        #region Static methods

        public static void WriteMacros(string[] macros)
        {
            // Check whether config file exists
            if (!IOServices.FileExists(s_configFilePath)) return;

            // Find marker definition
            string  content     = IOServices.ReadFile(s_configFilePath);
            var     lines       = content.Split('\n');
            int     pragmaIndex = Array.FindIndex(lines, (item) => item.StartsWith("#pragma", StringComparison.InvariantCultureIgnoreCase));
            if (pragmaIndex == -1)
            {
                DebugLogger.LogError(CoreLibraryDomain.Default, "Unknown error.");
                return;
            }

            // Copy contents existing before marker definition
            var     newLines    = new List<string>();
            for (int iter = 0; iter <= pragmaIndex; iter++)
            {
                newLines.Add(lines[iter]);
            }
            newLines.Add("");

            // Add specified macro symbols
            for (int uIter = 0; uIter < macros.Length; uIter++)
            {
                var     definition  = string.Format("#define {0} 1", macros[uIter]);
                newLines.Add(definition);
            }

            // Write new contents to file
            IOServices.CreateFile(s_configFilePath, string.Join("\n", newLines.ToArray()));
        }

        #endregion
    }
}
#endif