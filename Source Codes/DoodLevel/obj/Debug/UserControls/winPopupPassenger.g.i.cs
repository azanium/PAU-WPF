﻿#pragma checksum "..\..\..\UserControls\winPopupPassenger.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CCA083DA48646D5D34CF9D0549F51080"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DevComponents.WpfRibbon;
using Microsoft.Windows.Controls;
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
    /// winPopupPassenger
    /// </summary>
    public partial class winPopupPassenger : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\UserControls\winPopupPassenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevComponents.WpfRibbon.ButtonDropDown btnLoadPicture;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\UserControls\winPopupPassenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cbxEditMode;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\UserControls\winPopupPassenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgPreview;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\UserControls\winPopupPassenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Windows.Controls.DataGrid dgPassenger;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PAU;component/usercontrols/winpopuppassenger.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\winPopupPassenger.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btnLoadPicture = ((DevComponents.WpfRibbon.ButtonDropDown)(target));
            
            #line 22 "..\..\..\UserControls\winPopupPassenger.xaml"
            this.btnLoadPicture.Click += new System.Windows.RoutedEventHandler(this.btnLoadPicture_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cbxEditMode = ((System.Windows.Controls.CheckBox)(target));
            
            #line 23 "..\..\..\UserControls\winPopupPassenger.xaml"
            this.cbxEditMode.Click += new System.Windows.RoutedEventHandler(this.cbxEditMode_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.imgPreview = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.dgPassenger = ((Microsoft.Windows.Controls.DataGrid)(target));
            
            #line 29 "..\..\..\UserControls\winPopupPassenger.xaml"
            this.dgPassenger.RowEditEnding += new System.EventHandler<Microsoft.Windows.Controls.DataGridRowEditEndingEventArgs>(this.dgPassenger_RowEditEnding);
            
            #line default
            #line hidden
            
            #line 33 "..\..\..\UserControls\winPopupPassenger.xaml"
            this.dgPassenger.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dgPassenger_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

