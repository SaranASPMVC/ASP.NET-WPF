﻿#pragma checksum "..\..\Extractwatermark.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "512FEA834CDAA99F21146372CCD29B37AA57D20E26DF73C570E36061A57180C3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using System.Windows.Shell;


namespace WPFWatermarking {
    
    
    /// <summary>
    /// Extractwatermark
    /// </summary>
    public partial class Extractwatermark : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\Extractwatermark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\Extractwatermark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_watermarked;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\Extractwatermark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cb_browseorg;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\Extractwatermark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_original;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\Extractwatermark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_log;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\Extractwatermark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_text;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\Extractwatermark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_encryptkey;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\Extractwatermark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cb_close;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\Extractwatermark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cb_preview;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\Extractwatermark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_log;
        
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
            System.Uri resourceLocater = new System.Uri("/WPFWatermarking;component/extractwatermark.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Extractwatermark.xaml"
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
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\Extractwatermark.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.img_watermarked = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.cb_browseorg = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\Extractwatermark.xaml"
            this.cb_browseorg.Click += new System.Windows.RoutedEventHandler(this.cb_browseorg_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.img_original = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.txt_log = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.lbl_text = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.txt_encryptkey = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.cb_close = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\Extractwatermark.xaml"
            this.cb_close.Click += new System.Windows.RoutedEventHandler(this.cb_close_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.cb_preview = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\Extractwatermark.xaml"
            this.cb_preview.Click += new System.Windows.RoutedEventHandler(this.cb_preview_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.lbl_log = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

