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
using System.IO;
using cope;
using cope.IO;
using ModTool.Core.PlugIns;

namespace ModTool.Core
{
    /// <summary>
    /// Stores/loads the configuration file for plugins.
    /// </summary>
    static public class ConfigManager
    {
        private static XmlConfig s_pluginsConfig;
        private static string s_sConfigFilePath;

        static public bool SetupConfigSystem(string configFilePath)
        {
            ModManager.ApplicationExit += ModManagerApplicationExit;
            LoggingManager.SendMessage("ConfigManager - Setting up config system...");
            if (!File.Exists(configFilePath))
                LoggingManager.SendMessage("ConfingManager - No plugins.config found, creating one from scratch");

            s_sConfigFilePath = configFilePath;
            FileStream config = null;
            try
            {
                config = File.Open(configFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                s_pluginsConfig = XmlConfigReader.Read(config);
            }
            catch (Exception e)
            {
                LoggingManager.SendMessage("ConfigManager - Failed to set up config system from file: " + configFilePath);
                LoggingManager.HandleException(e);
                return false;
            }
            finally
            {
                if (config != null)
                    config.Close();
            }
            LoggingManager.SendMessage("ConfigManager - Config system set up successfully!");
            return true;
        }

        static void ModManagerApplicationExit()
        {
            FileStream configFile = null;
            try
            {
                if (s_pluginsConfig != null)
                {
                    configFile = File.Open(s_sConfigFilePath, FileMode.OpenOrCreate, FileAccess.Write,
                                                      FileShare.Read);
                    XmlConfigWriter.Write(s_pluginsConfig, configFile);
                    configFile.Flush();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.SendMessage("ConfigManager - Could not write config file");
                LoggingManager.HandleException(ex);
                UIHelper.ShowError("Failed to save configuration file. See Log file for more information.");
            }
            finally
            {
                if (configFile != null)
                    configFile.Close();
            }
        }

        static public void PlugInSetValue(ModToolPlugin tool, string key, string val)
        {
            ConfigSection pluginSection = GetSectionForPlugin(tool);
            pluginSection[key] = val;
        }

        static public string PlugInGetValue(ModToolPlugin tool, string key)
        {
            ConfigSection pluginSection = GetSectionForPlugin(tool);
            if (pluginSection.ContainsValue(key))
                return pluginSection[key];
            return null;
        }

        static public void PlugInRemoveValue(ModToolPlugin tool, string key)
        {
            ConfigSection pluginSection = GetSectionForPlugin(tool);
            pluginSection.RemoveValue(key);
        }

        static private ConfigSection GetSectionForPlugin(ModToolPlugin tool)
        {
            string sectionName = tool.GetType().Name;
            
            if (!s_pluginsConfig.ContainsValue(sectionName))
                s_pluginsConfig[sectionName] = new ConfigSection(sectionName);
            return s_pluginsConfig[sectionName];
        }
    }
}