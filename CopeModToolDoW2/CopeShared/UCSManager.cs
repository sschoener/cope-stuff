/*
Copyright (c) 2011 Sebastian Schoener

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.IO;
using cope.DawnOfWar2;
using cope;

namespace ModTool.Core
{
    static public class UCSManager
    {
        #region fields

        const string DOW2_UCS_FILE_NAME = "DOW2.ucs";

        static UCSStrings s_dow2UCS;
        static UCSStrings s_modUCS;
        static uint s_nextIndex;

        #endregion fields

        #region events

        public static event Action<uint, string> StringAdded;
        public static event Action<uint> StringRemoved;
        public static event Action<uint, string> StringModified;

        #endregion

        #region methods

        public static void Init()
        {
            LoggingManager.SendMessage("UCSManager - Initializing...");
            ModManager.ModLoaded += OnModLoaded;
            ModManager.ModUnloaded += OnModUnloaded;
            ModManager.ModLanguageChanged += OnModLanguageChanged;
        }

        public static bool HasString(uint index)
        {
            if (s_modUCS != null && s_modUCS.HasString(index))
                return true;
            return s_dow2UCS == null ? false : s_dow2UCS.HasString(index);
        }

        public static string GetString(uint index)
        {
            if (s_modUCS.HasString(index))
                return s_modUCS[index];
            if (s_dow2UCS != null && s_dow2UCS.HasString(index))
                return s_dow2UCS[index];
            return null;
        }

        public static uint AddString(string text)
        {
            return s_modUCS.AddString(text);
        }

        public static bool AddString(string text, uint index)
        {
            return s_modUCS.AddString(index, text);
        }

        public static bool ModifyString(string text, uint index)
        {
            return s_modUCS.ModifyString(index, text);
        }

        public static void ModifyOrAddString(string text, uint index)
        {
            s_modUCS.ModifyOrAdd(index, text);
        }

        public static bool RemoveString(uint index)
        {
            return s_modUCS.RemoveString(index);
        }

        public static void SaveUCS()
        {
            if (s_modUCS == null || s_modUCS.StringCount <= 0)
            {
                LoggingManager.SendMessage("UCSManager - UCS file is empty, nothing to save.");
                return;
            }
            LoggingManager.SendMessage("UCSManager - Saving UCS file for current mod...");
            FileStream ucs = null;
            try
            {
                ucs = File.Create(ModUCSPath);
                UCSWriter.Write(s_modUCS, ucs);
            }
            catch (Exception ex)
            {
                LoggingManager.SendMessage("UCSManager - Failed to save UCS file!");
                LoggingManager.HandleException(ex);
                UIHelper.ShowError("Failed to save UCS file! See the log for more details!");
                return;
            }
            finally
            {
                if (ucs != null)
                    ucs.Close();
            }
            LoggingManager.SendMessage("UCSManager - Successfully saved UCS file!");
        }

        public static void ReloadModUCS()
        {
            LoggingManager.SendMessage("UCSManager - Reloading Mod-UCS file");
            s_nextIndex = 0;
            LoadModUCS();
            LoggingManager.SendMessage("UCSManager - Finished reloading");
        }

        public static IEnumerable<KeyValuePair<uint, string>> GetStrings()
        {
            return s_modUCS;
        }

        static void LoadModUCS()
        {
            LoggingManager.SendMessage(
                "UCSManager - UCS file for current mod " + (File.Exists(ModUCSPath)
                                                                ? "found."
                                                                : "not found, will create a new one."));
            FileStream ucs = null;
            try
            {
                if (File.Exists(ModUCSPath))
                {
                    ucs = File.Open(ModUCSPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    s_modUCS = UCSReader.Read(ucs);
                }
                else
                    s_modUCS = new UCSStrings();
            }
            catch (CopeDoW2Exception ex)
            {
                LoggingManager.HandleException(ex);
                UIHelper.ShowError(ex.Message);
                ModManager.RequestAppExit(ex.Message);
            }
            finally
            {
                if (ucs != null)
                    ucs.Close();
            }

            if (s_modUCS.NextIndex > s_nextIndex)
                s_nextIndex = s_modUCS.NextIndex;
            else
                s_modUCS.NextIndex = s_nextIndex;
            s_modUCS.StringAdded += OnStringAdded;
            s_modUCS.StringModified += OnStringModified;
            s_modUCS.StringRemoved += OnStringRemoved;
        }

        static void LoadDoW2UCS()
        {
            if (File.Exists(DoW2UCSPath))
            {
                LoggingManager.SendMessage("UCSManager - UCS file for vanilla DoW2 found.");
                if (s_dow2UCS == null)
                {
                    FileStream ucs = null;
                    try
                    {
                        ucs = File.Open(DoW2UCSPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        s_dow2UCS = UCSReader.Read(ucs);
                    }
                    catch (CopeDoW2Exception ex)
                    {
                        LoggingManager.HandleException(ex);
                        UIHelper.ShowError(ex.Message);
                        ModManager.RequestAppExit(ex.Message);
                    }
                    finally
                    {
                        if (ucs != null)
                            ucs.Close();
                    }
                }
            }
            else
                LoggingManager.SendMessage("UCSManager - UCS file for vanilla DoW2 not found; we might operate in some strange place other than the DoW2 directory.");
        }

        #endregion methods

        #region properties

        public static uint NextIndex
        {
            get
            {
                if (s_modUCS != null)
                    return s_modUCS.NextIndex;
                return s_nextIndex;
            }
            set
            {
                if (s_modUCS != null)
                {
                    s_modUCS.NextIndex = value;
                    s_nextIndex = s_modUCS.NextIndex;
                }
                else
                    s_nextIndex = value;
            }
        }

        public static string ModUCSPath
        {
            get { return UCSDirectory + ModManager.ModName + ".ucs"; }
        }

        public static string DoW2UCSPath
        {
            get { return UCSDirectory + DOW2_UCS_FILE_NAME;  }
        }

        public static string UCSDirectory
        {
            get
            {
                return ModManager.GameDirectory + "GameAssets\\Locale\\" + ModManager.Language + '\\';
            }
        }

        #endregion properties

        #region eventhandlers

        static void OnModUnloaded()
        {
            SaveUCS();
            if (s_modUCS != null)
            {
                s_modUCS.StringAdded -= OnStringAdded;
                s_modUCS.StringModified -= OnStringModified;
                s_modUCS.StringRemoved -= OnStringRemoved;
                s_modUCS = null;
            }
            s_nextIndex = 0;
        }

        static void OnModLoaded()
        {
            LoggingManager.SendMessage("UCSManager - Trying to load UCS files...");
            LoadModUCS();
            LoadDoW2UCS();
            LoggingManager.SendMessage("UCSManager - Done loading UCS files!");
        }

        static void OnModLanguageChanged(object sender, string language)
        {
            s_dow2UCS = null;
        }

        static void OnStringRemoved(uint index)
        {
            if (StringRemoved != null)
                StringRemoved(index);
        }

        static void OnStringModified(uint index, string text)
        {
            if (StringModified != null)
                StringModified(index, text);
        }

        static void OnStringAdded(uint index, string text)
        {
            if (StringAdded != null)
                StringAdded(index, text);
        }

        #endregion eventhandlers
    }
}