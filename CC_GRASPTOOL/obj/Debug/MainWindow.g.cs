﻿#pragma checksum "..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "06B7ABFCBF60DAE44452331E3C38809AFE1A94AE"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
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


namespace CC_GRASPTOOL {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ui_listview_ck;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ui_rtext_return;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ui_rtext_ckinput;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ui_rtext_urlinput;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ui_text_numinput;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ui_text_bodyinput;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ui_req_type;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ui_text_url_append;
        
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
            System.Uri resourceLocater = new System.Uri("/CC_GRASPTOOL;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            
            #line 7 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_Clear);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 8 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_HandReq);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 9 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_ReadConf);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 10 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_ConfigReq);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ui_listview_ck = ((System.Windows.Controls.ListView)(target));
            
            #line 11 "..\..\MainWindow.xaml"
            this.ui_listview_ck.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ui_listview_ck_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ui_rtext_return = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.ui_rtext_ckinput = ((System.Windows.Controls.TextBox)(target));
            
            #line 39 "..\..\MainWindow.xaml"
            this.ui_rtext_ckinput.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.onText_reqCookie_changed);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ui_rtext_urlinput = ((System.Windows.Controls.TextBox)(target));
            
            #line 50 "..\..\MainWindow.xaml"
            this.ui_rtext_urlinput.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.onText_reqUrlSource_changed);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ui_text_numinput = ((System.Windows.Controls.TextBox)(target));
            
            #line 62 "..\..\MainWindow.xaml"
            this.ui_text_numinput.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.onText_reqNum_changed);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ui_text_bodyinput = ((System.Windows.Controls.TextBox)(target));
            
            #line 76 "..\..\MainWindow.xaml"
            this.ui_text_bodyinput.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.onText_reqBody_changed);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ui_req_type = ((System.Windows.Controls.ComboBox)(target));
            
            #line 84 "..\..\MainWindow.xaml"
            this.ui_req_type.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.onUireqtypeSelChenged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.ui_text_url_append = ((System.Windows.Controls.TextBox)(target));
            
            #line 98 "..\..\MainWindow.xaml"
            this.ui_text_url_append.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.onText_reqUrlAdd_changed);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

