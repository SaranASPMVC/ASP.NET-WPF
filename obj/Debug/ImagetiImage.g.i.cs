﻿#pragma checksum "..\..\ImagetiImage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C7A41527C9E7297DBB7BC856A586B8DB7DA858A559DA279CD5BD5C50FC6C60CB"
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
    /// ImagetiImage
    /// </summary>
    public partial class ImagetiImage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\ImagetiImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\ImagetiImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_original;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\ImagetiImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cb_browseorg;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\ImagetiImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_watermark;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\ImagetiImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cb_watermark;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\ImagetiImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_preview;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\ImagetiImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cb_close;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\ImagetiImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cb_saveas;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\ImagetiImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cb_preview;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\ImagetiImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_xposition;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\ImagetiImage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_yposition;
        
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
            System.Uri resourceLocater = new System.Uri("/WPFWatermarking;component/imagetiimage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ImagetiImage.xaml"
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
            
            #line 44 "..\..\ImagetiImage.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.img_original = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.cb_browseorg = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\ImagetiImage.xaml"
            this.cb_browseorg.Click += new System.Windows.RoutedEventHandler(this.cb_browseorg_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.img_watermark = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.cb_watermark = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\ImagetiImage.xaml"
            this.cb_watermark.Click += new System.Windows.RoutedEventHandler(this.cb_watermark_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.img_preview = ((System.Windows.Controls.Image)(target));
            return;
            case 7:
            this.cb_close = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\ImagetiImage.xaml"
            this.cb_close.Click += new System.Windows.RoutedEventHandler(this.cb_close_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cb_saveas = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\ImagetiImage.xaml"
            this.cb_saveas.Click += new System.Windows.RoutedEventHandler(this.cb_saveas_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.cb_preview = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\ImagetiImage.xaml"
            this.cb_preview.Click += new System.Windows.RoutedEventHandler(this.cb_preview_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.txt_xposition = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.txt_yposition = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
