﻿#pragma checksum "..\..\..\UserControls\winMySqlConnection.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E5A5AA3C1A815E741906441F0B063CB9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5448
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DevComponents.WpfRibbon;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PAU.UserControls {
    
    
    /// <summary>
    /// winMySqlConnection
    /// </summary>
    public partial class winMySqlConnection : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\UserControls\winMySqlConnection.xaml"
        internal System.Windows.Controls.TextBox server;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\UserControls\winMySqlConnection.xaml"
        internal System.Windows.Controls.TextBox username;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\UserControls\winMySqlConnection.xaml"
        internal System.Windows.Controls.TextBox password;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\UserControls\winMySqlConnection.xaml"
        internal System.Windows.Controls.TextBox database;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PAU;component/usercontrols/winmysqlconnection.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\winMySqlConnection.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.server = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.username = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.password = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.database = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 38 "..\..\..\UserControls\winMySqlConnection.xaml"
            ((DevComponents.WpfRibbon.ButtonDropDown)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonDropDown_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
