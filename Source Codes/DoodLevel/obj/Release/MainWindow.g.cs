﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EEF856466B18975437CD4A20A7B7EEFE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5448
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DevComponents.WpfDock;
using DevComponents.WpfEditors;
using DevComponents.WpfRibbon;
using Microsoft.Windows.Controls;
using PAU.Controllers;
using PAU.UserControls;
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


namespace PAU {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : DevComponents.WpfRibbon.RibbonWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\MainWindow.xaml"
        internal DevComponents.WpfRibbon.Ribbon MainRibbon;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\MainWindow.xaml"
        internal DevComponents.WpfRibbon.RibbonBar ribbonBarClipboard;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\MainWindow.xaml"
        internal DevComponents.WpfRibbon.RibbonBar ribbonBarDPO;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\MainWindow.xaml"
        internal DevComponents.WpfRibbon.RibbonBar ribbonBarTool;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\MainWindow.xaml"
        internal DevComponents.WpfEditors.IntegerInput edtRowPerPage;
        
        #line default
        #line hidden
        
        
        #line 191 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Primitives.StatusBarItem _statusServer;
        
        #line default
        #line hidden
        
        
        #line 205 "..\..\MainWindow.xaml"
        internal DevComponents.WpfDock.DockSite AppDock;
        
        #line default
        #line hidden
        
        
        #line 213 "..\..\MainWindow.xaml"
        internal DevComponents.WpfDock.SplitPanel rightPanel;
        
        #line default
        #line hidden
        
        
        #line 214 "..\..\MainWindow.xaml"
        internal DevComponents.WpfDock.DockWindowGroup dockWindowGroup;
        
        #line default
        #line hidden
        
        
        #line 215 "..\..\MainWindow.xaml"
        internal DevComponents.WpfDock.DockWindow rightPanelTool;
        
        #line default
        #line hidden
        
        
        #line 234 "..\..\MainWindow.xaml"
        internal DevComponents.WpfDock.SplitPanel renderView;
        
        #line default
        #line hidden
        
        
        #line 237 "..\..\MainWindow.xaml"
        internal DevComponents.WpfDock.DockWindowGroup dockGroup;
        
        #line default
        #line hidden
        
        
        #line 241 "..\..\MainWindow.xaml"
        internal DevComponents.WpfDock.DockWindow renderWindow;
        
        #line default
        #line hidden
        
        
        #line 249 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.DockPanel renderingDock;
        
        #line default
        #line hidden
        
        
        #line 251 "..\..\MainWindow.xaml"
        internal PAU.UserControls.ucPassenger dgPassenger;
        
        #line default
        #line hidden
        
        
        #line 285 "..\..\MainWindow.xaml"
        internal PAU.UserControls.ucDPO dgDPO;
        
        #line default
        #line hidden
        
        
        #line 302 "..\..\MainWindow.xaml"
        internal PAU.UserControls.ucNationality dgNationality;
        
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
            System.Uri resourceLocater = new System.Uri("/PAU;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            this.MainRibbon = ((DevComponents.WpfRibbon.Ribbon)(target));
            return;
            case 2:
            this.ribbonBarClipboard = ((DevComponents.WpfRibbon.RibbonBar)(target));
            return;
            case 3:
            this.ribbonBarDPO = ((DevComponents.WpfRibbon.RibbonBar)(target));
            return;
            case 4:
            this.ribbonBarTool = ((DevComponents.WpfRibbon.RibbonBar)(target));
            return;
            case 5:
            this.edtRowPerPage = ((DevComponents.WpfEditors.IntegerInput)(target));
            
            #line 151 "..\..\MainWindow.xaml"
            this.edtRowPerPage.ValueChanged += new System.Windows.RoutedEventHandler(this.edtRowPerPage_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this._statusServer = ((System.Windows.Controls.Primitives.StatusBarItem)(target));
            return;
            case 7:
            this.AppDock = ((DevComponents.WpfDock.DockSite)(target));
            return;
            case 8:
            this.rightPanel = ((DevComponents.WpfDock.SplitPanel)(target));
            return;
            case 9:
            this.dockWindowGroup = ((DevComponents.WpfDock.DockWindowGroup)(target));
            return;
            case 10:
            this.rightPanelTool = ((DevComponents.WpfDock.DockWindow)(target));
            return;
            case 11:
            this.renderView = ((DevComponents.WpfDock.SplitPanel)(target));
            return;
            case 12:
            this.dockGroup = ((DevComponents.WpfDock.DockWindowGroup)(target));
            return;
            case 13:
            this.renderWindow = ((DevComponents.WpfDock.DockWindow)(target));
            return;
            case 14:
            this.renderingDock = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 15:
            this.dgPassenger = ((PAU.UserControls.ucPassenger)(target));
            return;
            case 16:
            this.dgDPO = ((PAU.UserControls.ucDPO)(target));
            return;
            case 17:
            this.dgNationality = ((PAU.UserControls.ucNationality)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
