﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1008
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ffd.Presentation.LeadManager {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Leadmanager : global::System.Configuration.ApplicationSettingsBase {
        
        private static Leadmanager defaultInstance = ((Leadmanager)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Leadmanager())));
        
        public static Leadmanager Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("001")]
        public string EmailTemplateNo {
            get {
                return ((string)(this["EmailTemplateNo"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\FfdApplications\\FanFriendlyDesigns\\Ffd.Presentation.LeadManager\\email")]
        public string EmailSrcDir {
            get {
                return ((string)(this["EmailSrcDir"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DebuggingMode {
            get {
                return ((bool)(this["DebuggingMode"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int CheckEmailIntervalSecs {
            get {
                return ((int)(this["CheckEmailIntervalSecs"]));
            }
        }
    }
}
