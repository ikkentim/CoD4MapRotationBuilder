﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cod4MapRotationBuilder.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("mp_convoy;mp_backlot;mp_bloc;mp_countdown;mp_crash;mp_crossfire;mp_citystreets;mp" +
            "_farm;mp_overgrown;mp_pipeline;mp_shipment;mp_strike;mp_showdown;mp_vacant;mp_ca" +
            "rgoship;mp_crash_snow;mp_broadcast;mp_carentan;mp_creek;mp_killhouse")]
        public string StockMaps {
            get {
                return ((string)(this["StockMaps"]));
            }
            set {
                this["StockMaps"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("war:Team Deathmatch;sd:Search & Destroy;sab:Sabotage;koth:Headquarters;dom:Domina" +
            "tion;dm:Free for All;ftag:Freeze Tag")]
        public string GameModes {
            get {
                return ((string)(this["GameModes"]));
            }
            set {
                this["GameModes"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Path {
            get {
                return ((string)(this["Path"]));
            }
            set {
                this["Path"] = value;
            }
        }
    }
}