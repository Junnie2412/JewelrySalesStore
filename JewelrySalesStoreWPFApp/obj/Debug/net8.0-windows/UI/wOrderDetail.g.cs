﻿#pragma checksum "..\..\..\..\UI\wOrderDetail.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A436C86B3127C7EA471009AFA3EDEDA238204F2D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using JewelrySalesStoreWPFApp.UI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace JewelrySalesStoreWPFApp.UI {
    
    
    /// <summary>
    /// wOrderDetail
    /// </summary>
    public partial class wOrderDetail : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 28 "..\..\..\..\UI\wOrderDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtOrderDetailId;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\UI\wOrderDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtOrderId;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\UI\wOrderDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUnitPrice;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\UI\wOrderDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTotalPrice;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\UI\wOrderDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtProductId;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\UI\wOrderDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtQuantity;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\UI\wOrderDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonSave;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\UI\wOrderDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonCancel;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\UI\wOrderDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid gridOrderDetail;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/JewelrySalesStoreWPFApp;component/ui/worderdetail.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UI\wOrderDetail.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtOrderDetailId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtOrderId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtUnitPrice = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtTotalPrice = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtProductId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtQuantity = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.ButtonSave = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\..\UI\wOrderDetail.xaml"
            this.ButtonSave.Click += new System.Windows.RoutedEventHandler(this.ButtonSave_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ButtonCancel = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\..\UI\wOrderDetail.xaml"
            this.ButtonCancel.Click += new System.Windows.RoutedEventHandler(this.ButtonCancel_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.gridOrderDetail = ((System.Windows.Controls.DataGrid)(target));
            
            #line 57 "..\..\..\..\UI\wOrderDetail.xaml"
            this.gridOrderDetail.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.grdOrderDetail_MouseDouble_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 10:
            
            #line 71 "..\..\..\..\UI\wOrderDetail.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.grdOrderDetail_ButtonEdit_Click);
            
            #line default
            #line hidden
            break;
            case 11:
            
            #line 72 "..\..\..\..\UI\wOrderDetail.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.grdOrderDetail_ButtonDelete_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

